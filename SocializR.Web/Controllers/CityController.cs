namespace SocializR.Web.Controllers;

public class CityController(ApplicationUnitOfWork _unitOfWork,
    ICityService _cityService, 
    IMapper _mapper) : BaseController(_mapper)
{
    [HttpGet]
    public async Task<JsonResult> Index(Guid id)
    {
        var cities = await _cityService.GetAllViewModelsAsync(id);

        return Json(cities);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<List<SelectListItem>> GetAllByCountyAsync(Guid id)
    {
        return await _cityService.GetSelectListByCountyAsync(id);
    }

    [HttpPost]
    public async Task<JsonResult> DeleteAsync(Guid id)
    {
        _cityService.Remove(id);

        if (!await _unitOfWork.SaveChangesAsync())
        {
            return Json(new { id = 1, message = "Ups, something happened!" });
        }

        return Json(new { id = 0, message = "City deleted successfuly!" });
    }

    [HttpPost]
    public async Task<IActionResult> EditAsync(Guid id, string name)
    {
        var city = _cityService.Get(id);

        if (city == null)
        {
            return NotFound();
        }

        city.Name = name;
        _cityService.Update(city);

        if (!await _unitOfWork.SaveChangesAsync())
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(string name, Guid countyId)
    {
        _cityService.Add(new City { CountyId = countyId, Name = name});

        if (! await _unitOfWork.SaveChangesAsync())
        {
            return InternalServerErrorView();
        }

        return Ok();
    }
}
