namespace SocializR.Web.Code.Mappers;

public class ViewProfileMapper : Profile
{
    public ViewProfileMapper()
    {
        CreateMap<User, ViewProfileViewModel>()
            .ForMember(dest => dest.City, opt =>
            {
                opt.PreCondition(src => src.City != null);
                opt.MapFrom(src => src.City.Name);
            })
            .ForMember(dest => dest.County, opt =>
            {
                opt.PreCondition(src => src.City.County != null);
                opt.MapFrom(src => src.City.County.Name);
            })
            .ForMember(dest => dest.Interests, opt =>
            {
                opt.PreCondition(src => src.UserInterests.Any());
                opt.MapFrom(src => src.UserInterests.Select(u => u.Interest.Id));
            })
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => ((GenderTypes)src.Gender).ToString()))
            .ForMember(dest => dest.FilePath, opt =>
            {
                opt.PreCondition(src => src.ProfilePhoto != null);
                opt.MapFrom(u => u.ProfilePhoto.FilePath);
            })
            .ForMember(dest => dest.NrOfFriends, opt => opt.MapFrom(src => src.FriendsFirstUser.Count()))
            .ForMember(dest => dest.NrOfPhotos, opt => opt.MapFrom(src => src.Albums.SelectMany(a => a.Media).Count()))
            .ForMember(dest => dest.NrOfPosts, opt => opt.MapFrom(src => src.Posts.Count()));
    }
}
