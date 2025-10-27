using AutoMapper;
using MailKit;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;
using System.Text.Json;
using WebAPIDB.Data.Entities.Identity;
using WebReddkaApi.Interfaces;
using WebReddkaApi.Models.Account;

namespace WebReddkaApi.Services;

public class AccountService(IJWTTokenService tokenService,
        UserManager<UserEntity> userManager,
        IMapper mapper,
        IMediaService mediaService,
        IConfiguration configuration) : IAccountService
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

    public async Task<Dictionary<string, string>?> LoginByGoogle(string token)
    {
        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.GetAsync(configuration["GoogleUserInfo"] ?? "https://www.googleapis.com/oauth2/v2/userinfo");

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();

        var googleUser = JsonSerializer.Deserialize<GoogleAccountModel>(json);

        var existingUser = await userManager.FindByEmailAsync(googleUser!.Email);
        if (existingUser != null)
        {
            var userLoginGoogle = await userManager.FindByLoginAsync("Google", googleUser.GoogleId);

            if (userLoginGoogle == null)
            {
                await userManager.AddLoginAsync(existingUser, new UserLoginInfo("Google", googleUser.GoogleId, "Google"));
            }

            var jwtToken = await tokenService.CreateTokenAsync(existingUser);
            await userManager.UpdateAsync(existingUser);
            return jwtToken;
        }
        else
        {
            var user = mapper.Map<UserEntity>(googleUser);

            if (!String.IsNullOrEmpty(googleUser.Picture))
            {
                user.Image = await mediaService.SaveImageFromUrlAsync(googleUser.Picture);
            }

            var result = await userManager.CreateAsync(user);
            if (result.Succeeded)
            {

                result = await userManager.AddLoginAsync(user, new UserLoginInfo("Google", googleUser.GoogleId, "Google"));

                await userManager.AddToRoleAsync(user, "User");
                var jwtToken = await tokenService.CreateTokenAsync(user);
                await userManager.UpdateAsync(user);
                return jwtToken;
            }
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
