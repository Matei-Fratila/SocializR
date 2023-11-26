using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocializR.Entities.DTOs;
using SocializR.Services.SearchServices;
using SocializR.Web.Code.Base;

namespace SocializR.Web.Controllers
{
    public class SearchController : BaseController
    {
        private readonly SearchService searchService;

        public SearchController(IMapper mapper, SearchService searchService)
            :base(mapper)
        {
            this.searchService = searchService;
        }

        [HttpGet]
        public JsonResult Search(int page, string search)
        {
            var data = searchService.SearchResults(page, 5, search);

            return Json(data);
        }
    }
}