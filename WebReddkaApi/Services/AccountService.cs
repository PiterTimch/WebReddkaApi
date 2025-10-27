using AutoMapper;
using MailKit;
using Microsoft.AspNetCore.Identity;
using WebAPIDB.Data.Entities.Identity;
using WebReddkaApi.Interfaces;
using WebReddkaApi.Models.Account;

namespace WebReddkaApi.Services;

public class AccountService(IJWTTokenService tokenService,
        UserManager<UserEntity> userManager,
        IMapper mapper,
        IMediaService mediaService) : IAccountService
{
    public async Task<Dictionary<string, string>?> LoginAsync(LoginModel model)
    {
        var user = await userManager.FindByNameAsync(model.Username);

        if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
        {
            var tokens = await tokenService.CreateTokenAsync(user);
            await userManager.UpdateAsync(user);
            return tokens;
        }

        return null;
    }


    public async Task<Dictionary<string, string>?> RegisterAsync(RegisterModel model)
    {
        var user = mapper.Map<UserEntity>(model);
        if (model.ImageFile != null)
        {
            user.Image = await mediaService.SaveImageAsync(model.ImageFile);
        }
        var result = await userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");
            var token = await tokenService.CreateTokenAsync(user);
            return token;
        }
        return null;
    }
}
