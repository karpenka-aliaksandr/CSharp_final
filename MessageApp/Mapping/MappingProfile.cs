using AutoMapper;
using MessageApp.DTO;
using MessageApp.Models;

namespace MessageApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Message, MessageViewModel>().ReverseMap();
        }
    }
}
