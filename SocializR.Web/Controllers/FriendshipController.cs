using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SocializR.Entities.DTOs.Common;
using SocializR.Entities.DTOs.Friend;
using SocializR.Services;
using SocializR.Services.FriendshipServices;
using SocializR.Web.Code.Base;
using SocializR.Web.Code.Configuration;
using System.Collections.Generic;
using X.PagedList;

namespace SocializR.Web.Controllers
{
    [Authorize]
    public class FriendshipController : BaseController
    {
        private readonly FriendshipService friendshipService;
        private readonly FriendRequestService friendRequestService;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly AppSettings configuration;

        public FriendshipController(IOptions<AppSettings> configuration, IHostingEnvironment hostingEnvironment, FriendshipService friendshipService, FriendRequestService friendRequestService, IMapper mapper)
             : base(mapper)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.friendshipService = friendshipService;
            this.friendRequestService = friendRequestService;
            this.configuration = configuration.Value;
        }

        [HttpGet]
        public IActionResult Index(int? page)
        {
            var pageIndex = (page ?? 1) - 1;
            var friends = friendshipService.GetFriends(pageIndex, configuration.UsersPerPage, out int totalUserCount);
            var model = new StaticPagedList<UserVM>(friends, pageIndex + 1, configuration.UsersPerPage, totalUserCount);

            return View(model);
        }

        [HttpPost]
        public IActionResult AddFriend(string id)
        {
            var result = friendshipService.AddFriend(id);

            if (result)
            {
                friendRequestService.DeleteFriendRequest(id);
            }

            return RedirectToAction("Get", "Profile", new { id });
        }

        [HttpPost]
        public IActionResult Unfriend(string id)
        {
            var result = friendshipService.Unfriend(id);
            if (!result)
            {
                return InternalServerErrorView();
            }

            return RedirectToAction("Get", "Profile", new { id });
        }
    }
}