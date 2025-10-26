using AutoMapper;
using WebAPIDB.Data.Entities.Identity;
using WebReddkaApi.Models.Account;
using WebReddkaApi.Models.Seeders;

namespace WebReddkaApi.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<SeederUserModel, UserEntity>()
            .ForMember(opt => opt.UserName, opt => opt.MapFrom(x => x.Email));

        CreateMap<RegisterModel, UserEntity>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Email));
    }
}