using AutoMapper;
using LearnWords.DAL.Entities;
using LearnWords.Services.Models;

namespace LearnWords.Services.Services;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Word, WordDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap();
    }
}
