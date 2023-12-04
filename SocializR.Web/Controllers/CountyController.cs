namespace SocializR.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class CountyController(ApplicationUnitOfWork _unitOfWork,
    ICountyService _countyService,
    IMapper _mapper) : BaseController(_mapper)
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new CountiesViewModel { Counties = await _countyService.GetAllAsync() };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        _countyService.Remove(id);

        if (!await _unitOfWork.SaveChangesAsync())
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> EditAsync(Guid id, string name, string shortname)
    {
        var county = await _countyService.GetAsync(id);
        county.Name = name;
        county.ShortName = shortname;
        _countyService.Update(county);

        if (!await _unitOfWork.SaveChangesAsync())
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(string name, string shortName)
    {
        _countyService.Add(new County { Name = name, ShortName = shortName });

        if (!await _unitOfWork.SaveChangesAsync())
        {
            return InternalServerErrorView();
        }

        return Ok();
    }


}