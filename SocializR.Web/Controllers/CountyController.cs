namespace SocializR.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class CountyController(ICountyService _countyService, 
    IMapper _mapper) : BaseController(_mapper)
{
    [HttpGet]
    public IActionResult Index()
    {
        var model = new CountiesViewModel { Counties = _countyService.GetAllCities() };
        return View(model);
    }

    [HttpPost]
    public JsonResult Delete(string countyId)
    {
        var result = _countyService.Delete(countyId);

        switch (result)
        {
            case (1):
                return Json(new { id = 1, message = "Ups, Something Happened!" });
            case (2):
                return Json(new { id = 2, message = "County has cities! Delete all cities before trying to delete county" });
            default:
                return Json(new { id = 0, message = "County deleted successfuly" });
        }
    }

    [HttpPost]
    public IActionResult Edit(string id, string name, string shortname)
    {
        var result = _countyService.Update(id, name, shortname);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpPost]
    public IActionResult Add(string name, string shortName)
    {
        var result = _countyService.Create(name, shortName);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }


}