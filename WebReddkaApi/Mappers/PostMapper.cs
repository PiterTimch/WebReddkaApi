using AutoMapper;
using WebReddkaApi.Data.Entities;
using WebReddkaApi.Models.Posts;

namespace WebReddkaApi.Mappers;

public class PostMapper : Profile
{
    public PostMapper()
    {
        CreateMap<PostEntity, PostItemModel>()
            .ForMember(src => src.TopicName, opt => opt.MapFrom(x => x.Topic.Name));

        CreateMap<PostCreateModel, PostEntity>()
            .ForMember(src => src.Image, opt => opt.Ignore())
            .ForMember(src => src.Video, opt => opt.Ignore())
            .ForMember(src => src.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
    }
}
