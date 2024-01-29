using SocializR.Models.ViewModels;

namespace SocializR.Services;

public class CountyService(ApplicationUnitOfWork unitOfWork, IMapper _mapper)
    : BaseService<County, CountyService>(unitOfWork), ICountyService
{
    public async Task<List<CountyViewModel>> GetAllAsync()
        => await UnitOfWork.Counties.Query
            .OrderBy(c => c.ShortName)
            .ProjectTo<CountyViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task<List<SelectItem>> GetSelectItemsAsync()
        => await UnitOfWork.Counties.Query
            .OrderBy(c => c.ShortName)
            .ProjectTo<SelectItem>(_mapper.ConfigurationProvider)
            .ToListAsync();

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
