using WebBot.Models;

namespace WebBot.Services;

public class AuthState
{
    public event Action? OnChange;

    public string? Token { get; private set; }
    public Guid? UserId { get; private set; }
    public string? FullName { get; private set; }
    public string? Phone { get; private set; }
    public string? Role { get; private set; }

    public bool IsLoggedIn => !string.IsNullOrEmpty(Token);
    public bool IsClient => IsLoggedIn && Role == "Client";
    public bool IsCourier => IsLoggedIn && Role == "Courier";

    public void Set(AuthResponse resp)
    {
        Token = resp.Token;
        UserId = resp.UserId;
        FullName = resp.FullName;
        Phone = resp.Phone;
        Role = resp.Role;
        Notify();
    }

    public void Logout()
    {
        Token = null;
        UserId = null;
        FullName = null;
        Phone = null;
        Role = null;
        Notify();
    }

    private void Notify() => OnChange?.Invoke();
}
