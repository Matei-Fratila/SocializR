namespace SocializR.Web.Controllers;

[AllowAnonymous]
public class AccountController(ICountyService _countyService,
    IMapper _mapper,
    UserManager<User> _userManager,
    SignInManager<User> _signInManager) : BaseController(_mapper)
{
    [HttpGet]
    public async Task<IActionResult> LoginAsync(string returnUrl)
    {
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        var model = new LogInViewModel
        {
            ReturnUrl = returnUrl
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginAsync(LogInViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            if (string.IsNullOrEmpty(model.ReturnUrl))
            {
                return RedirectToAction(nameof(HomeController.IndexAsync).RemoveAsyncSuffix(),
                    nameof(HomeController).RemoveControllerSuffix());
            }

            return Redirect(model.ReturnUrl);
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt");

        return View();
        //return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).RemoveControllerSuffix());
    }

    [HttpGet]
    public async Task<IActionResult> LogoutAsync()
    {
        //await LogOutCookie();

        await _signInManager.SignOutAsync();

        return RedirectToAction(nameof(LoginAsync).RemoveAsyncSuffix());
    }

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        var counties = await _countyService.GetSelectListAsync();

        if (counties == null)
        {
            return InternalServerErrorView();
        }

        var model = new RegisterViewModel
        {
            Counties = counties
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Counties = await _countyService.GetSelectListAsync();
            return View(model);
        }

        var user = _mapper.Map<User>(model);
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
            return RedirectToAction(nameof(HomeController.IndexAsync).RemoveAsyncSuffix(),
                nameof(HomeController).RemoveControllerSuffix());
        }

        foreach (IdentityError error in result.Errors)
        {
            ModelState.AddModelError("Password", error.Description);
        }

        model.Counties = await _countyService.GetSelectListAsync();
        return View(model);

        //var encription = new Pbkdf2();
        //entity.Password = encription.CalculateHash(entity.Password);

        //var result = AccountService.Register(entity);

        //if (!result)
        //{
        //    return InternalServerErrorView();
        //}
    }

    //private async Task LogInCookie(User user)
    //{
    //    var claims = new List<Claim>
    //    {
    //        new Claim(ClaimTypes.Name, user.FirstName),
    //        new Claim(ClaimTypes.Email, user.Email),
    //    };
    //    user.UserRoles
    //        .ToList()
    //        .ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r.Role.Name)));

    //    var identity = new ClaimsIdentity(claims, "Cookies");
    //    var principal = new ClaimsPrincipal(identity);

    //    await HttpContext.SignInAsync(
    //            scheme: "SocializRCookies",
    //            principal: principal);
    //}

    private async Task LogOutCookie()
    {
        await HttpContext.SignOutAsync(scheme: "SocializRCookies");
    }
}
