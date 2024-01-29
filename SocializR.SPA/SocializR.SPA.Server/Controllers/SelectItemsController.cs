using SocializR.Models.Enums;
using SocializR.Models.ViewModels;

namespace SocializR.SPA.Server.Controllers;

[ApiController]
[AllowAnonymous]
public class SelectItemsController(ICityService _cityService,
    ICountyService _countyService,
    IInterestService _interestService) : ControllerBase
{
    [HttpGet("/counties")]
    public async Task<List<SelectItem>> GetCounties()
    {
        var counties = await _countyService.GetSelectItemsAsync();

        return counties;
    }

    [HttpGet("/cities")]
    public async Task<List<SelectItem>> GetCities(Guid id)
    {
        var cities = await _cityService.GetSelectItemsByCountyAsync(id);

        return cities;
    }

    [HttpGet("/interests")]
    public async Task<List<SelectItem>> GetInterests()
    {
        var interests = await _interestService.GetSelectItemsAsync();

        return interests;
    }

    [HttpGet("/genders")]
    public async Task<List<SelectItem>> GetGenders()
    {
        return Enum.GetValues(typeof(GenderTypes))
            .Cast<GenderTypes>()
            .Select(x => new SelectItem 
            { 
                Label = x.ToString(), 
                Value = ((int)x).ToString() 
            })
            .ToList();
    }
}
