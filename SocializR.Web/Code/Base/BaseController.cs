﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace SocializR.Web.Code.Base
{
    public class BaseController : Controller
    {
        protected readonly IMapper mapper;

        public BaseController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IActionResult InternalServerErrorView()
        {
            return View("InternalServerError");
        }

        public IActionResult NotFoundView()
        {
            return View("NotFound");
        }
        public IActionResult ForbidView()
        {
            return View("Forbid");
        }

        public IActionResult UserNotFoundView()
        {
            return View("UserNotFound");
        }
    }
}
