using SocializR.Models.ViewModels;

namespace SocializR.Services.Mappers;

public class ViewProfileMapper : Profile
{
    public ViewProfileMapper()
    {
        CreateMap<User, ProfileViewModel>()
            .ForMember(dest => dest.City, opt =>
            {
                opt.PreCondition(src => src.City != null);
                opt.MapFrom(src => new SelectItem { Label = src.City.Name, Value = src.City.Id.ToString() });
            })
            .ForMember(dest => dest.County, opt =>
            {
                opt.PreCondition(src => src.City.County != null);
                opt.MapFrom(src => new SelectItem { Label = src.City.County.Name, Value = src.City.County.Id.ToString() });
            })
            .ForMember(dest => dest.Interests, opt =>
            {
                opt.PreCondition(src => src.UserInterests.Any());
                opt.MapFrom(src => src.UserInterests.Select(u => new SelectItem { Value = u.Interest.Id.ToString(), Label = u.Interest.Name }));
            })
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => 
                new SelectItem { Value = ((int)src.Gender).ToString(), Label = ((GenderTypes)src.Gender).ToString() }))
            .ForMember(dest => dest.Avatar, opt =>
            {
                opt.PreCondition(src => src.ProfilePhoto != null);
                opt.MapFrom(u => u.ProfilePhoto.FileName);
            })
            .ForMember(dest => dest.NrOfFriends, opt => opt.MapFrom(src => src.FriendsFirstUser.Count()))
            .ForMember(dest => dest.NrOfPhotos, opt => opt.MapFrom(src => src.Albums.SelectMany(a => a.Media).Count()))
            .ForMember(dest => dest.NrOfPosts, opt => opt.MapFrom(src => src.Posts.Count()));
            //.ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src.Posts));
    }
}
