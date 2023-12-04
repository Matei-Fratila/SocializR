namespace SocializR.Services;

public class CityService(ApplicationUnitOfWork unitOfWork, IMapper _mapper) 
    : BaseService<City, CityService>(unitOfWork), ICityService
{
    public async Task<List<City>> GetAllAsync(Guid countyId)
        => await UnitOfWork.Cities.Query
            .Where(c => c.CountyId == countyId)
            .OrderBy(u => u.Name)
            .ToListAsync();

    public async Task<List<CityViewModel>> GetAllViewModelsAsync(Guid countyId)
        => await UnitOfWork.Cities.Query
            .Where(u => u.CountyId == countyId)
            .OrderBy(u => u.Name)
            .ProjectTo<CityViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task<List<SelectListItem>> GetSelectListByCountyAsync(Guid countyId)
        => await UnitOfWork.Cities.Query
            .Where(u => u.CountyId == countyId)
            .OrderBy(u => u.Name)
            .Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
            .ToListAsync();
}
