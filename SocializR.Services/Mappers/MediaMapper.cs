namespace SocializR.Services.Mappers;

public class MediaMapper : Profile
{
    public MediaMapper()
    {
        CreateMap<Media, MediaViewModel>()
            .ForMember(m => m.CreatedDate, opt => opt.MapFrom(m => m.CreatedDate.TimeAgo()));
        CreateMap<MediaViewModel, Media>();
    }
}
