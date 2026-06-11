using System.Text;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Implementations;
using Services.Interfaces;
using Services.Settings;
using Telegram.Bot;
using WebAPI.Hubs;
using WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5000");

#region CONFIGS

var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
builder.Services.AddSingleton(jwtSettings!);

var botToken = builder.Configuration["Bot:BotToken"];
if (!string.IsNullOrWhiteSpace(botToken))
{
    builder.Services.AddSingleton<ITelegramBotClient>(
        _ => new TelegramBotClient(botToken));
}

#endregion

#region DB

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion

#region SERVICES
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<StoreService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ServiceService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<OrderService>();

#endregion

#region AUTH

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings!.Secret))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;

            if (!string.IsNullOrEmpty(accessToken) &&
                path.StartsWithSegments("/hubs"))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        }
    };
});

#endregion

#region CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

#endregion

#region SIGNALR + SWAGGER

builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Bekobod 24 API",
        Version = "v1",
        Description = "Super App Marketplace API"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

#endregion

var app = builder.Build();

#region DB MIGRATION

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
    
}

#endregion

#region MIDDLEWARE PIPELINE

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<OrderHub>("/hubs/orders");

var bot = app.Services.GetRequiredService<ITelegramBotClient>();
var botService = new TelegramBotService(bot);
botService.Start();
#endregion

#region WEBBOT STATIC FILES

var webBotRoot = Path.Combine(
    Directory.GetCurrentDirectory(),
    "..",
    "WebBot",
    "publish",
    "wwwroot");

if (Directory.Exists(webBotRoot))
{
    var fileProvider = new PhysicalFileProvider(webBotRoot);

    var contentTypeProvider = new FileExtensionContentTypeProvider();
    contentTypeProvider.Mappings[".wasm"] = "application/wasm";
    contentTypeProvider.Mappings[".br"] = "application/octet-stream";

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = fileProvider,
        ContentTypeProvider = contentTypeProvider,
        ServeUnknownFileTypes = true
    });

    app.MapFallbackToFile("index.html", new StaticFileOptions
    {
        FileProvider = fileProvider
    });
}

#endregion

app.Run();