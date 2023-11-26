using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocializR.Services.InterestServices;
using SocializR.Web.Code.Base;
using SocializR.Entities.DTOs.Interest;

namespace SocializR.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class InterestController : BaseController
    {
        private readonly InterestService interestService;

        public InterestController(IMapper mapper, InterestService interestService)
            : base(mapper)
        {
            this.interestService = interestService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var interests = interestService.GetAllInterests();

            return View(interests);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var model = interestService.GetEditModel(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditInterestVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = interestService.EditInterest(model);

            if (!result)
            {
                return InternalServerErrorView();
            }

            return RedirectToAction("Index", "Interest");

        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new EditInterestVM()
            {
                Interests = interestService.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EditInterestVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            interestService.AddInterest(model);

            return RedirectToAction("Index", "Interest");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var result = interestService.DeleteInterest(id);

            if (!result)
            {
                return InternalServerErrorView();
            }

            return RedirectToAction("Index", "Interest");
        }
    }
}