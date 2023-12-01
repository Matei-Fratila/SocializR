﻿using Utils;

namespace SocializR.Web.Controllers;

public class AccountController(CountyService _countyService,
    IMapper _mapper,
    UserManager<User> _userManager,
    SignInManager<User> _signInManager) : BaseController(_mapper)
{
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        var model = new LogInViewModel
        {
            ReturnUrl = returnUrl
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LogInViewModel model)
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
                    return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).RemoveControllerSuffix());

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

        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).RemoveControllerSuffix());
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        //await LogOutCookie();

        await _signInManager.SignOutAsync();

        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult Register()
    {
        var counties = _countyService.GetSelectCounties();

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
            model.Counties = _countyService.GetSelectCounties();
            return View(model);
        }

        var user = _mapper.Map<User>(model);
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).RemoveControllerSuffix());
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
