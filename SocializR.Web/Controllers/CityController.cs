namespace SocializR.Web.Controllers;

public class CityController(ICityService _cityService, 
    IMapper _mapper) : BaseController(_mapper)
{
    [HttpGet]
    public JsonResult Index(Guid id)
    {
        var cities = _cityService.GetAllByCountyId(id);

        return Json(cities);
    }

    [HttpGet]
    [AllowAnonymous]
    public List<SelectListItem> GetAllByCounty(Guid id)
    {
        return _cityService.GetAllByCounty(id);
    }

    [HttpPost]
    public JsonResult Delete(string cityId)
    {
        var result = _cityService.Delete(cityId);

        if (!result)
        {
            return Json(new { id = 1, message = "Ups, something happened!" });
        }

        return Json(new { id = 0, message = "City deleted successfuly!" });
    }

    [HttpPost]
    public IActionResult Edit(string id, string name)
    {
        var result = _cityService.EditCity(id, name);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpPost]
    public IActionResult Add(string name, string countyId)
    {
        var result = _cityService.Create(name, countyId);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }
}
