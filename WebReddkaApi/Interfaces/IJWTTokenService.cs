using WebAPIDB.Data.Entities.Identity;

namespace WebReddkaApi.Interfaces;

public interface IJWTTokenService
{
    Task<string> CreateTokenAsync(UserEntity user);
}
