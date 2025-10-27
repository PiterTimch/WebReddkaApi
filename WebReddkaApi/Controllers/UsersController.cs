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
        var result = await accountService.LoginAsync(model);
        return Ok(new
        {
            access = result?["access"],
            refresh = result?["refresh"]
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterModel model)
    {
        var result = await accountService.RegisterAsync(model);
        if (result == null)
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
            access = result?["access"],
            refresh = result?["refresh"]
        });
    }
}