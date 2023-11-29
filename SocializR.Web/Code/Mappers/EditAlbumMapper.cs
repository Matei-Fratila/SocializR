namespace SocializR.Web.Code.Mappers;

public class EditAlbumMapper : Profile
{
    public EditAlbumMapper()
    {
        CreateMap<AlbumViewModel, CreateAlbumViewModel>();
    }
}
