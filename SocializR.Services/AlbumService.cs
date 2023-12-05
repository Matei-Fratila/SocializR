namespace SocializR.Services;

public class AlbumService(ApplicationUnitOfWork unitOfWork, 
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

    public void Update(AlbumViewModel model)
    {
        var album = Get(model.Id);

        if (album == null)
        {
            return;
        }

        album.Name = model.Name;
    }
}