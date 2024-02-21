using SocializR.Models.Enums;
using SocializR.Models.ViewModels;

namespace SocializR.SPA.Server.Controllers;

[ApiController]
[AllowAnonymous]
public class SelectItemsController(ICityService _cityService,
    ICountyService _countyService,
    IInterestService _interestService) : ControllerBase
{
    [HttpGet("/Counties")]
    public async Task<IResult> GetCountiesAsync()
    {
        var counties = await _countyService.GetSelectItemsAsync();

        return Results.Ok(counties);
    }

    [HttpGet("/Cities")]
    public async Task<IResult> GetCitiesAsync([FromQuery] Guid id)
    {
        var cities = await _cityService.GetSelectItemsByCountyAsync(id);

        return Results.Ok(cities);
    }

    [HttpGet("/Interests")]
    public async Task<IResult> GetInterestsAsync()
    {
        var interests = await _interestService.GetSelectItemsAsync();

        return Results.Ok(interests);
    }

    [HttpGet("/Genders")]
    public IResult GetGenders()
    {
        var genders = Enum.GetValues(typeof(GenderTypes))
            .Cast<GenderTypes>()
            .Select(x => new SelectItem 
            { 
                Label = x.ToString(), 
                Value = ((int)x).ToString() 
            })
            .ToList();

        return Results.Ok(genders);
    }
}
