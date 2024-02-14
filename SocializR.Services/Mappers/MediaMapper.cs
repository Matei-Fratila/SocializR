namespace SocializR.Services.Mappers;

public class MediaMapper : Profile
{
    public MediaMapper()
    {
        CreateMap<Media, MediaViewModel>()
            .ForMember(m => m.CreatedDateDisplay, opt => opt.MapFrom(m => m.CreatedDate.TimeAgo()))
            .ForMember(a => a.LastModifiedDateDisplay, opt => opt.MapFrom(u => u.LastModifiedDate.HasValue ? u.LastModifiedDate.Value.TimeAgo() : String.Empty));
    }
}
