using AutoMapper;
using WebReddkaApi.Data.Entities;
using WebReddkaApi.Models.Topics;

namespace WebReddkaApi.Mappers;

public class TopicMapper : Profile
{
    public TopicMapper()
    {
        CreateMap<TopicEntity, TopicItemModel>();
    }
}
