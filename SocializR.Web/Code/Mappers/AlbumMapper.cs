namespace SocializR.Web.Code.Mappers;

public class AlbumMapper : Profile
{
    public AlbumMapper()
    {
        CreateMap<Media, MediaModel>();

        CreateMap<Album, AlbumVM>()
            .ForMember(a => a.NrOfImages, opt => opt.MapFrom(u => u.Media.Count))
            .ForMember(a => a.CoverId, opt => opt.MapFrom(u => u.Media.OrderByDescending(c => c.Id).Select(c => c.Id).FirstOrDefault()))
            .ForMember(a => a.CoverFilePath, opt => opt.MapFrom(u => u.Media.OrderByDescending(c => c.Id).Select(c => c.FilePath).FirstOrDefault()));

        CreateMap<Album, EditAlbumVM>();

    }
}
