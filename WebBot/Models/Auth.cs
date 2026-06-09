namespace WebBot.Models;

public class LoginRequest
{
    public string Phone { get; set; } = "";
    public string Password { get; set; } = "";
}

public class RegisterRequest
{
    public string FullName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Password { get; set; } = "";
}

public class AuthResponse
{
    public Guid UserId { get; set; }
    public string FullName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Role { get; set; } = "";
    public string Token { get; set; } = "";
}
