namespace SocializR.Services.Mappers;

public class MediaMapper : Profile
{
    public MediaMapper()
    {
        CreateMap<Media, MediaViewModel>()
            .ForMember(m => m.CreatedDate, opt => opt.MapFrom(m => m.CreatedDate.TimeAgo()))
            .ForMember(a => a.LastModifiedDate, opt => opt.MapFrom(u => u.LastModifiedDate.HasValue ? u.LastModifiedDate.Value.TimeAgo() : String.Empty));
    }
}
