namespace SocializR.Web.Controllers;

public class AccountController(CountyService _countyService,
    CityService _cityService,
    IMapper _mapper,
    UserManager<User> _userManager,
    SignInManager<User> _signInManager) : BaseController(_mapper)
{
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        var model = new LoginVM
        {
            ReturnUrl = returnUrl
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM model)
    {
        //var context = Request;

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user != null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(model.ReturnUrl))
                    return RedirectToAction("Index", "Home");

                return Redirect(model.ReturnUrl);
            }
        }

        //var encription = new Pbkdf2();

        //var user = AccountService.Login(model.Email);

        //if (user == null)
        //{
        //    model.AreCredentialsInvalid = true;
        //    return View(model);
        //}

        //if (!encription.CheckMatch(user.Password, model.Password))
        //{
        //    model.AreCredentialsInvalid = true;
        //    return View(model);
        //}

        //await LogInCookie(user);

        //if (model.ReturnUrl != null)
        //{
        //    return Redirect(model.ReturnUrl);
        //}

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        //await LogOutCookie();

        await _signInManager.SignOutAsync();

        return RedirectToAction("Login", "Account");
    }

    [HttpGet]
    public IActionResult Register()
    {
        var counties = _countyService.GetSelectCounties();

        if (counties == null)
        {
            return InternalServerErrorView();
        }

        var model = new RegisterVM
        {
            Counties = counties
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVM model)
    {
        if (!ModelState.IsValid)
        {
            model.Counties = _countyService.GetSelectCounties();
            return View(model);
        }

        var user = _mapper.Map<User>(model);
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
            return RedirectToAction("Index", "Home");
        }

        foreach(IdentityError error in result.Errors)
        {
            ModelState.AddModelError("Password", error.Description);
        }

        model.Counties = _countyService.GetSelectCounties();
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
