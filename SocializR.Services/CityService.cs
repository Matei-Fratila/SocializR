using Microsoft.Extensions.Caching.Memory;
using SocializR.Models.ViewModels;

namespace SocializR.Services;

public class CityService(ApplicationUnitOfWork unitOfWork,
    IMapper _mapper,
    IMemoryCache _memoryCache)
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

    public async Task<List<SelectItem>> GetSelectItemsByCountyAsync(Guid countyId)
    {
        var cachedValue = await _memoryCache.GetOrCreateAsync($"cities-byCountyId-{countyId}",
            async cacheEntry =>
            {
                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7);
                return await UnitOfWork.Cities.Query
                    .Where(c => c.CountyId == countyId)
                    .OrderBy(u => u.Name)
                    .ProjectTo<SelectItem>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            });

        return cachedValue;
    }

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
