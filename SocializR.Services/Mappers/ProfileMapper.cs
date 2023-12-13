namespace SocializR.Services.Mappers;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        CreateMap<User, ProfileViewModel>()
            .ForMember(p => p.CountyId, opt => opt.MapFrom(u => u.City.CountyId))
            .ForMember(p => p.ProfilePhoto, opt => opt.Ignore())
            .ForMember(p => p.FileName, opt => opt.MapFrom(u => u.ProfilePhoto.FileName));

        CreateMap<Guid, UserInterest>()
            .ForMember(dest => dest.InterestId, opt => opt.MapFrom(src => src));

        CreateMap<ProfileViewModel, User>()
            .ForMember(dest => dest.UserInterests, opt => opt.MapFrom(src => src.Interests))
            .ForMember(p => p.ProfilePhoto, opt => opt.Ignore());
    }
}
