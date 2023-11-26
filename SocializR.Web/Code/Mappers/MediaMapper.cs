namespace SocializR.Web.Code.Mappers;

public class MediaMapper : Profile
{
    public MediaMapper()
    {
        CreateMap<Media, MediaModel>();
    }
}
