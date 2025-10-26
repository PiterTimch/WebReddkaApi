using WebReddkaApi.Models.Account;

namespace WebReddkaApi.Interfaces;

public interface IAccountService
{
    public Task<string> LoginAsync(LoginModel model);
    public Task<string> RegisterAsync(RegisterModel model);
}
