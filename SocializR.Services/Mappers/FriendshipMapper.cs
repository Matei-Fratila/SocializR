namespace SocializR.Services.Mappers;

public class FriendshipMapper : Profile
{
    public FriendshipMapper()
    {
        CreateMap<Friendship, UserViewModel>()
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SecondUser.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.SecondUser.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.SecondUser.LastName))
            .ForMember(dest => dest.ProfilePhoto, opt => opt.MapFrom(src => src.SecondUser.ProfilePhoto.FileName));

        CreateMap<FriendRequest, FriendrequestViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RequesterUser.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.RequesterUser.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.RequesterUser.LastName))
            .ForMember(dest => dest.ProfilePhotoId, opt => opt.MapFrom(src => src.RequesterUser.ProfilePhotoId));
    }
}
