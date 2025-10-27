using WebAPIDB.Data.Entities.Identity;

namespace WebReddkaApi.Interfaces;

public interface IJWTTokenService
{
    Task<Dictionary<string, string>> CreateTokenAsync(UserEntity user);
}
