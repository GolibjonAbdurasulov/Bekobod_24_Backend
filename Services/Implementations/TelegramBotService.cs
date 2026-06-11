
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
namespace Services.Implementations;
using Telegram.Bot.Types.ReplyMarkups;
public class TelegramBotService
{
    private readonly ITelegramBotClient _bot;

    public TelegramBotService(ITelegramBotClient bot)
    {
        _bot = bot;
    }

    public void Start()
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        _bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions
        );
    }



    private async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken ct)
    {
        if (update.Message is { } msg)
        {
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithWebApp(
                    "🚀 Open App",
                    new WebAppInfo
                    {
                        Url = "https://ion-eden-arrival-fruits.trycloudflare.com"
                    }
                )
            });

            await bot.SendMessage(
                chatId: msg.Chat.Id,
                text: "Welcome to Bekobod 24 🚀",
                replyMarkup: keyboard,
                cancellationToken: ct
            );
        }
    }

    private Task HandleErrorAsync(ITelegramBotClient bot, Exception ex, CancellationToken ct)
    {
        Console.WriteLine(ex.Message);
        return Task.CompletedTask;
    }
}