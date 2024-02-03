namespace SocializR.Services;

public class AlbumService(ApplicationUnitOfWork unitOfWork, 
    IMediaService _mediaService,
    IMapper _mapper) : BaseService<Album, AlbumService>(unitOfWork), IAlbumService
{
    public async Task<Album> GetAsync(string name, Guid userId)
        => await UnitOfWork.Albums.Query
            .Where(a => a.Name == name && a.UserId == userId)
            .FirstOrDefaultAsync();

    public async Task<List<AlbumViewModel>> GetAllAsync(Guid userId)
        => await UnitOfWork.Albums.Query
            .Where(u => u.UserId == userId)
            .ProjectTo<AlbumViewModel>(_mapper.ConfigurationProvider)
            .OrderBy(i => i.Name)
            .ToListAsync();

    public async Task<AlbumViewModel> GetViewModelAsync(Guid id)
        => await UnitOfWork.Albums.Query
            .Where(a => a.Id == id)
            .Include(a => a.Media)
            .ProjectTo<AlbumViewModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

    public async Task Update(AlbumViewModel model)
    {
        var album = await GetAsync(model.Id);

        if (album == null)
        {
            return;
        }

        album.Name = model.Name;
        Update(album);

        foreach(var media in model.Media)
        {
            var entity = await _mediaService.GetAsync(media.Id);
            entity.Caption = media.Caption;
            _mediaService.Update(entity);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var album = await UnitOfWork.Albums.Query
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync();

        if(album == null)
        {
            return;
        }

        Remove(album);
    }
}