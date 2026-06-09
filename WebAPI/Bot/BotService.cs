using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace WebAPI.Bot;

public class BotService : BackgroundService
{
    private readonly ITelegramBotClient _bot;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<BotService> _logger;

    public BotService(ITelegramBotClient bot, IServiceScopeFactory scopeFactory, ILogger<BotService> logger)
    {
        _bot = bot;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        var me = await _bot.GetMeAsync(ct);
        _logger.LogInformation("Bot started: @{Username}", me.Username);

        var receiver = new ReceiverOptions { AllowedUpdates = [UpdateType.Message] };

        _bot.StartReceiving(HandleUpdate, HandleError, receiver, ct);
        await Task.Delay(-1, ct);
    }

    private async Task HandleUpdate(ITelegramBotClient bot, Telegram.Bot.Types.Update update, CancellationToken ct)
    {
        if (update.Message is not { } msg) return;

        try
        {
            using var scope = _scopeFactory.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<CommandHandler>();
            await handler.HandleMessage(msg, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "HandleUpdate error for chat {ChatId}", msg.Chat.Id);
        }
    }

    private Task HandleError(ITelegramBotClient bot, Exception ex, CancellationToken ct)
    {
        _logger.LogError(ex, "Polling error");
        return Task.CompletedTask;
    }
}
