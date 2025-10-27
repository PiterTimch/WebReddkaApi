namespace WebReddkaApi.Models.Account;

public class LoginModel
{
    /// <summary>
    /// Нік користувача
    /// </summary>
    /// <example>admin</example>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Пароль користувача
    /// </summary>
    /// <example>Admin123!</example>
    public string Password { get; set; } = string.Empty;
}
