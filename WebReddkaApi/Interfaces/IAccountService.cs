using WebReddkaApi.Models.Account;

namespace WebReddkaApi.Interfaces;

public interface IAccountService
{
    public Task<Dictionary<string, string>?> LoginAsync(LoginModel model);
    public Task<Dictionary<string, string>?> RegisterAsync(RegisterModel model);
    public Task<Dictionary<string, string>?> LoginByGoogle(string token);
}
