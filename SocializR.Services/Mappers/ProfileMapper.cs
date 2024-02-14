namespace SocializR.Services.Mappers;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        CreateMap<User, EditProfile>()
            .ForMember(p => p.County, opt => opt.MapFrom(u => u.City.CountyId))
            .ForMember(p => p.Avatar, opt => opt.Ignore());

        CreateMap<Guid, UserInterest>()
            .ForMember(dest => dest.InterestId, opt => opt.MapFrom(src => src));

        CreateMap<EditProfile, User>()
            .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => new Guid((src.City.Value))))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => Enum.Parse(typeof(GenderTypes), src.Gender.Label)))
            .ForMember(dest => dest.UserInterests, opt => opt.MapFrom(src => src.Interests.Select(x => new UserInterest { InterestId = new Guid(x.Value) })))
            .ForMember(p => p.ProfilePhoto, opt => opt.Ignore())
            .ForMember(p => p.City, opt => opt.Ignore());
    }
}
