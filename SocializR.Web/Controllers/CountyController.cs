using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocializR.Entities.DTOs.Map;
using SocializR.Services;
using SocializR.Services.CityServices;
using SocializR.Web.Code.Base;

namespace SocializR.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CountyController : BaseController
    {
        private readonly CountyService countyService;
        private readonly CityService cityService;

        public CountyController(CountyService countyService, CityService cityService, IMapper mapper)
           : base(mapper)
        {
            this.countyService = countyService;
            this.cityService = cityService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var counties = countyService.GetCounties();
            var model = new CountiesVM { Counties = counties };
            return View(model);
        }

        [HttpPost]
        public JsonResult Delete(string countyId)
        {
            var result = countyService.DeleteCounty(countyId);

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
            var result = countyService.EditCounty(id, name, shortname);

            if (!result)
            {
                return InternalServerErrorView();
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Add(string name, string shortName)
        {
            var result = countyService.AddCounty(name, shortName);

            if (!result)
            {
                return InternalServerErrorView();
            }

            return Ok();
        }


    }
}