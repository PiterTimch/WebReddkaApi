using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebReddkaApi.Models.Account;

public class RegisterModel
{
    /// <summary>
    /// Електрона пошта користувача
    /// </summary>
    /// <example>Федір</example>
    [FromForm(Name = "first_name")]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Електрона пошта користувача
    /// </summary>
    /// <example>Лупашко</example>
    [FromForm(Name = "last_name")]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Електрона пошта користувача
    /// </summary>
    /// <example>fedir@example.com</example>
    [FromForm(Name = "email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Пароль користувача
    /// </summary>
    /// <example>Admin123!</example>
    [FromForm(Name = "password")]
    public string Password { get; set; } = string.Empty;

    [FromForm(Name = "username")]
    public string? Username { get; set; }

    [FromForm(Name = "image")]
    public IFormFile? ImageFile { get; set; } = null;
}
