using Microsoft.Extensions.Caching.Memory;
using SocializR.Models.ViewModels;

namespace SocializR.Services;

public class CountyService(ApplicationUnitOfWork unitOfWork,
    IMapper _mapper,
    IMemoryCache _memoryCache)
    : BaseService<County, CountyService>(unitOfWork), ICountyService
{
    public async Task<List<CountyViewModel>> GetAllAsync()
    {
        var cachedValue = await _memoryCache.GetOrCreateAsync("counties-all",
            async cacheEntry =>
            {
                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7);
                return await UnitOfWork.Counties.Query
                    .OrderBy(c => c.ShortName)
                    .ProjectTo<CountyViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            });

        return cachedValue;
    }

    public async Task<List<SelectItem>> GetSelectItemsAsync()
    {
        var cachedValue = await _memoryCache.GetOrCreateAsync("counties-select",
            async cacheEntry =>
            {
                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7);
                return await UnitOfWork.Counties.Query
                    .OrderBy(c => c.ShortName)
                    .ProjectTo<SelectItem>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            });

        return cachedValue;
    }

    public async Task<List<SelectListItem>> GetSelectListAsync()
        => await UnitOfWork.Counties.Query
            .OrderBy(c => c.ShortName)
            .Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
            .ToListAsync();
}
