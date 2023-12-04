namespace SocializR.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class InterestController(ApplicationUnitOfWork _unitOfWork,
    IMapper _mapper, 
    IInterestService _interestService) : BaseController(_mapper)
{
    [HttpGet]
    public async Task<IActionResult> IndexAsync()
    {
        var interests = await _interestService.GetAllAsync();

        return View(interests);
    }

    [HttpGet]
    public async Task<IActionResult> EditAsync(Guid id)
    {
        var model = await _interestService.GetViewModelAsync(id);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAsync(EditInterestViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _interestService.EditAsync(model);

        if (!await _unitOfWork.SaveChangesAsync())
        {
            return InternalServerErrorView();
        }

        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public async Task<IActionResult> CreateAsync()
    {
        var model = new EditInterestViewModel()
        {
            Interests = await _interestService.GetSelectListAsync()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(EditInterestViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _interestService.Add(model);

        if (!await _unitOfWork.SaveChangesAsync())
        {
            return InternalServerErrorView();
        }

        return RedirectToAction(nameof(IndexAsync).RemoveAsyncSuffix());
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _interestService.DeleteAsync(id);

        if (!await _unitOfWork.SaveChangesAsync())
        {
            return InternalServerErrorView();
        }

        return RedirectToAction(nameof(Index));
    }
}