using System.Text;
using Infrastructure;
using Infrastructure.Repositories.CartItemRepositories;
using Infrastructure.Repositories.CartRepositories;
using Infrastructure.Repositories.FileRepositories;
using Infrastructure.Repositories.OrderItemRepositories;
using Infrastructure.Repositories.OrderRepositories;
using Infrastructure.Repositories.ProductRepositories;
using Infrastructure.Repositories.ServiceRepositories;
using Infrastructure.Repositories.StoreRepositories;
using Infrastructure.Repositories.UserRepositories;
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

var apiSettings = builder.Configuration.GetSection("Api").Get<ApiSettings>() ?? new ApiSettings();
ApiSettings.BaseUrl = apiSettings.InstanceBaseUrl ?? "http://localhost:5000";

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

#region REPOSITORIES
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
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

if (!app.Environment.IsProduction())
    app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<OrderHub>("/hubs/orders");

var botTokenCfg = app.Configuration["Bot:BotToken"];
if (!string.IsNullOrWhiteSpace(botTokenCfg))
{
    var bot = app.Services.GetRequiredService<ITelegramBotClient>();
    var webAppUrlCfg = app.Configuration["Bot:WebAppUrl"] ?? "";
    var botService = new TelegramBotService(bot, webAppUrlCfg);
    botService.Start();
    Console.WriteLine("[BOT] Telegram bot started successfully");
}
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
