using SocializR.Models.ViewModels.Search;

namespace SocializR.Services.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, SearchUserViewModel>()
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));

        CreateMap<User, UserViewModel>()
            .ForMember(dest => dest.XP, opt => opt.MapFrom(src => src.Game.XP))
            .ForMember(dest => dest.ProfilePhoto, opt => opt.MapFrom(src => src.ProfilePhoto.FileName));
    }
}
