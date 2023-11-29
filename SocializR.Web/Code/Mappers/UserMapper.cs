namespace SocializR.Web.Code.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, SearchUserViewModel>()
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));

        CreateMap<User, UserViewModel>();
    }
}
