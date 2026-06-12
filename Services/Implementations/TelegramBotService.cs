using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Services.Implementations;

public class TelegramBotService
{
    private readonly ITelegramBotClient _bot;
    private readonly string _webAppUrl;

    public TelegramBotService(ITelegramBotClient bot, string webAppUrl)
    {
        _bot = bot;
        _webAppUrl = webAppUrl;
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
            Console.WriteLine($"[BOT] ChatId={msg.Chat.Id} | Username={msg.Chat.Username} | Text={msg.Text} | Type={msg.Type}");

            var text = msg.Text ?? "";

            if (text.StartsWith("/start"))
            {
                var keyboard = new InlineKeyboardMarkup(new[]
                {
                    InlineKeyboardButton.WithWebApp(
                        "Katalogni ochish",
                        new WebAppInfo { Url = _webAppUrl }
                    )
                });

                await bot.SendMessage(
                    chatId: msg.Chat.Id,
                    text: "Bekobod 24 botiga xush kelibsiz! Katalogni ochish uchun tugmani bosing.",
                    replyMarkup: keyboard,
                    cancellationToken: ct
                );
            }
            else if (text.StartsWith("/courier"))
            {
                var keyboard = new InlineKeyboardMarkup(new[]
                {
                    InlineKeyboardButton.WithWebApp(
                        "Kuryer panelini ochish",
                        new WebAppInfo { Url = $"{_webAppUrl}/courier" }
                    )
                });

                await bot.SendMessage(
                    chatId: msg.Chat.Id,
                    text: "Kuryer paneliga xush kelibsiz! Yetkazib berishni boshqarish uchun tugmani bosing.",
                    replyMarkup: keyboard,
                    cancellationToken: ct
                );
            }
            else
            {
                await bot.SendMessage(
                    chatId: msg.Chat.Id,
                    text: "Buyruqlar:\n/start - Katalogni ochish\n/courier - Kuryer paneli",
                    cancellationToken: ct
                );
            }
        }
        else if (update.CallbackQuery is { } cb)
        {
            Console.WriteLine($"[BOT] CallbackQuery ChatId={cb.Message?.Chat.Id} | User={cb.From.Username} | Data={cb.Data}");
        }
        else if (update.MyChatMember is { } mcm)
        {
            Console.WriteLine($"[BOT] MyChatMember ChatId={mcm.Chat.Id} | Status={mcm.NewChatMember.Status}");
        }
        else
        {
            Console.WriteLine($"[BOT] Unhandled update type={update.Type} | Id={update.Id}");
        }
    }

    private Task HandleErrorAsync(ITelegramBotClient bot, Exception ex, CancellationToken ct)
    {
        Console.WriteLine($"[BOT ERROR] {ex.Message}");
        return Task.CompletedTask;
    }
}
