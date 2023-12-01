namespace SocializR.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class InterestController(IMapper _mapper, 
    InterestService _interestService) : BaseController(_mapper)
{
    [HttpGet]
    public IActionResult Index()
    {
        var interests = _interestService.GetAllInterests();

        return View(interests);
    }

    [HttpGet]
    public IActionResult Edit(string id)
    {
        var model = _interestService.GetEditModel(id);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EditInterestViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = _interestService.EditInterest(model);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public IActionResult Create()
    {
        var model = new EditInterestViewModel()
        {
            Interests = _interestService.GetAll()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EditInterestViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _interestService.AddInterest(model);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(string id)
    {
        var result = _interestService.DeleteInterest(id);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return RedirectToAction(nameof(Index));
    }
}