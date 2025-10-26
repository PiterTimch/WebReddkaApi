using Microsoft.AspNetCore.Mvc;
using WebReddkaApi.Interfaces;
using WebReddkaApi.Models.Account;

namespace WebReddkaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IAccountService accountService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        string result = await accountService.LoginAsync(model);
        return Ok(new
        {
            Token = result
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterModel model)
    {
        string result = await accountService.RegisterAsync(model);
        if (string.IsNullOrEmpty(result))
        {
            return BadRequest(new
            {
                Status = 400,
                IsValid = false,
                Errors = new { Email = "Помилка реєстрації" }
            });
        }
        return Ok(new
        {
            Token = result
        });
    }
}