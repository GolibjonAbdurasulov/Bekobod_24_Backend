using Microsoft.JSInterop;

namespace WebBot.Services;

public class TelegramService
{
    private readonly IJSRuntime _js;
    private bool _isTelegram;

    public TelegramService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task InitAsync()
    {
        try
        {
            var user = await _js.InvokeAsync<object?>("TelegramWebApp.getUser");
            _isTelegram = user != null;
            if (_isTelegram)
                await _js.InvokeVoidAsync("TelegramWebApp.ready");
        }
        catch
        {
            _isTelegram = false;
        }
    }

    public bool IsTelegram => _isTelegram;

    public async Task CloseAsync()
    {
        if (!_isTelegram) return;
        await _js.InvokeVoidAsync("TelegramWebApp.close");
    }

    public async Task SendDataAsync(string data)
    {
        if (!_isTelegram) return;
        await _js.InvokeVoidAsync("TelegramWebApp.sendData", data);
    }

    public async Task ShowBackButtonAsync(bool show)
    {
        if (!_isTelegram) return;
        await _js.InvokeVoidAsync("TelegramWebApp.showBackButton", show);
    }

    public async Task<string?> GetUsernameAsync()
    {
        try
        {
            var user = await _js.InvokeAsync<TelegramUser?>("TelegramWebApp.getUser");
            return user?.Username ?? user?.FirstName;
        }
        catch
        {
            return null;
        }
    }

    public async Task<TelegramUser?> GetUserAsync()
    {
        try
        {
            return await _js.InvokeAsync<TelegramUser?>("TelegramWebApp.getUser");
        }
        catch
        {
            return null;
        }
    }

    public class TelegramUser
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? LanguageCode { get; set; }
    }
}
