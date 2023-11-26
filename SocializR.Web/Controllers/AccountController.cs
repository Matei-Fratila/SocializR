using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SocializR.Entities.DTOs.Account;
using SocializR.Entities;
using SocializR.Services;
using SocializR.Services.CityServices;
using SocializR.Services.UserServices;
using SocializR.Web.Code.Base;
using SocializR.Web.Code.Encription;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SocializR.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly CountyService countyService;
        private readonly CityService cityService;

        public AccountController(/*AccountService AccountService, */CountyService countyService, CityService cityService, IMapper mapper,
            UserManager<User> userManager, SignInManager<User> signInManager)
            : base(mapper)
        {
            //this.AccountService = AccountService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.countyService = countyService;
            this.cityService = cityService;
        }

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

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, true, true);
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

            await signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var counties = countyService.GetSelectCounties();

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
                model.Counties = countyService.GetSelectCounties();
                return View(model);
            }

            var user = mapper.Map<User>(model);
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
                return RedirectToAction("Index", "Home");
            }

            foreach(IdentityError error in result.Errors)
            {
                ModelState.AddModelError("Password", error.Description);
            }

            model.Counties = countyService.GetSelectCounties();
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
}
