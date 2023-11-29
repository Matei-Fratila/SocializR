namespace SocializR.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class CountyController(CountyService _countyService, 
    IMapper _mapper) : BaseController(_mapper)
{
    [HttpGet]
    public IActionResult Index()
    {
        var counties = _countyService.GetCounties();
        var model = new CountiesViewModel { Counties = counties };
        return View(model);
    }

    [HttpPost]
    public JsonResult Delete(string countyId)
    {
        var result = _countyService.DeleteCounty(countyId);

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
        var result = _countyService.EditCounty(id, name, shortname);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpPost]
    public IActionResult Add(string name, string shortName)
    {
        var result = _countyService.AddCounty(name, shortName);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }


}