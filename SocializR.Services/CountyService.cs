using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using SocializR.Models.ViewModels;
using System.Text.Json;

namespace SocializR.Services;

public class CountyService(ApplicationUnitOfWork unitOfWork,
    IMapper _mapper,
    IMemoryCache _memoryCache,
    IDistributedCache _distributedCache)
    : BaseService<County, CountyService>(unitOfWork), ICountyService
{
    public async Task<List<CountyViewModel>> GetAllAsync()
    {
        //using Memory Cache
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
        //Memory cache
        //var cachedValue = await _memoryCache.GetOrCreateAsync("counties-select",
        //    async cacheEntry =>
        //    {
        //        cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7);
        //        return await UnitOfWork.Counties.Query
        //            .OrderBy(c => c.ShortName)
        //            .ProjectTo<SelectItem>(_mapper.ConfigurationProvider)
        //            .ToListAsync();
        //    });

        //return cachedValue;

        //Distributed cache using Azure Redis cache
        var key = $"counties-select";
        var cachedValue = await _distributedCache.GetStringAsync(key);

        if (cachedValue == null)
        {
            var data = await UnitOfWork.Counties.Query
                .OrderBy(c => c.ShortName)
                .ProjectTo<SelectItem>(_mapper.ConfigurationProvider)
                .ToListAsync();

            await _distributedCache.SetStringAsync(key,
                JsonSerializer.Serialize(data),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
                });

            return data;
        }

        return JsonSerializer.Deserialize<List<SelectItem>>(cachedValue);
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
