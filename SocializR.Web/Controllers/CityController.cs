using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SocializR.Services.CityServices;
using SocializR.Web.Code.Base;
using System.Collections.Generic;

namespace SocializR.Web.Controllers
{
    public class CityController : BaseController
    {
        private readonly CityService cityService;

        public CityController(CityService cityService, IMapper mapper)
            : base(mapper)
        {
            this.cityService = cityService;
        }

        [HttpGet]
        public JsonResult Index(string id)
        {
            var cities = cityService.GetCitiesByCountyId(id);
            return Json(cities);
        }

        [HttpGet]
        [AllowAnonymous]
        public List<SelectListItem> GetAllByCounty(string id)
        {
            return cityService.GetCities(id);
        }

        [HttpPost]
        public JsonResult Delete(string cityId)
        {
            var result = cityService.Delete(cityId);

            if (!result)
            {
                return Json(new { id = 1, message = "Ups, something happened!" });
            }

            return Json(new { id = 0, message = "City deleted successfuly!" });
        }

        [HttpPost]
        public IActionResult Edit(string id, string name)
        {
            var result = cityService.EditCity(id, name);

            if (!result)
            {
                return InternalServerErrorView();
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Add(string name, string countyId)
        {
            var result = cityService.AddCity(name, countyId);

            if (!result)
            {
                return InternalServerErrorView();
            }

            return Ok();
        }
    }
}
