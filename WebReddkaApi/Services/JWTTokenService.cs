using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAPIDB.Data.Entities.Identity;
using WebReddkaApi.Interfaces;

namespace WebReddkaApi.Services;

public class JWTTokenService(IConfiguration configuration,
    UserManager<UserEntity> userManager) : IJWTTokenService
{
    public async Task<Dictionary<string, string>> CreateTokenAsync(UserEntity user)
    {
        string key = configuration["JWT:Key"]!;

        var claims = new List<Claim>
        {
            new Claim("email", user.Email),
            new Claim("image", user.Image != null? user.Image : ""),
            new Claim("id", user.Id.ToString()),
            new Claim("username", user.UserName!),
            new Claim("date_joined", user.DateCreated.ToString())
        };

        foreach (var role in await userManager.GetRolesAsync(user))
        {
            claims.Add(new Claim("role", role));
        }

        var keyBytes = System.Text.Encoding.UTF8.GetBytes(key);
        var signingKey = new SymmetricSecurityKey(keyBytes);

        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var sec = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: signingCredentials
        );

        var result = new Dictionary<string, string>();

        result["access"] = new JwtSecurityTokenHandler().WriteToken(sec);
        result["refresh"] = new JwtSecurityTokenHandler().WriteToken(sec);

        return result;
    }
}
