using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace WebAPI.Bot;

public class CommandHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly string _webAppUrl;
    private readonly ILogger<CommandHandler> _logger;

    public CommandHandler(ITelegramBotClient bot, IConfiguration config, ILogger<CommandHandler> logger)
    {
        _bot = bot;
        _webAppUrl = config["Bot:WebAppUrl"] ?? "";
        _logger = logger;
    }

    public async Task HandleMessage(Message msg, CancellationToken ct)
    {
        if (msg.Text is not { } text) return;
        var chatId = msg.Chat.Id;

        switch (text)
        {
            case "/start":
                await SendStartMenu(chatId, ct);
                break;
            case "/courier":
                await SendCourierMenu(chatId, ct);
                break;
        }
    }

    private async Task SendStartMenu(long chatId, CancellationToken ct)
    {
        await _bot.SendTextMessageAsync(chatId,
            "Welcome to Bekobod 24! 🛒\n\nChoose an option below:",
            cancellationToken: ct);

        await SendWebAppButton(chatId, "🛍 Open Catalog", _webAppUrl, ct);
    }

    private async Task SendCourierMenu(long chatId, CancellationToken ct)
    {
        await _bot.SendTextMessageAsync(chatId,
            "🚚 Courier panel. Use the button below to open the dashboard.",
            cancellationToken: ct);

        await SendWebAppButton(chatId, "🚚 Open Courier Panel", $"{_webAppUrl}/courier", ct);
    }

    private async Task SendWebAppButton(long chatId, string buttonText, string url, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(url) || url.StartsWith("REPLACE"))
        {
            await _bot.SendTextMessageAsync(chatId,
                "⚠ WebApp not configured yet. Set Bot:WebAppUrl in appsettings.json (ngrok HTTPS URL).",
                cancellationToken: ct);
            return;
        }

        try
        {
            var markup = new InlineKeyboardMarkup(new[]
            {
                new[] { InlineKeyboardButton.WithWebApp(buttonText, new WebAppInfo { Url = url }) },
            });

            await _bot.SendTextMessageAsync(chatId, "Open app:", replyMarkup: markup, cancellationToken: ct);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to send WebApp button (invalid URL: {Url})", url);
            await _bot.SendTextMessageAsync(chatId,
                "⚠ Open app button failed. Make sure WebApp URL is public HTTPS.",
                cancellationToken: ct);
        }
    }
}
