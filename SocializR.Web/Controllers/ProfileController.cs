﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocializR.Services;
using SocializR.Services.CityServices;
using SocializR.Services.InterestServices;
using SocializR.Services.UserServices;
using SocializR.Web.Code.Base;
using Microsoft.AspNetCore.Authorization;
using SocializR.Entities.Enums;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SocializR.Services.MediaServices;
using System.Threading.Tasks;
using SocializR.Services.AlbumServices;
using SocializR.Entities.DTOs.Common;
using SocializR.Entities.DTOs.Profile;
using SocializR.Services.PostServices;
using SocializR.Services.FriendshipServices;
using Microsoft.AspNetCore.Identity;
using SocializR.Entities;
using Common.Interfaces;
using System;

namespace SocializR.Web.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ProfileService profileService;
        private readonly CountyService countyService;
        private readonly CityService cityService;
        private readonly InterestService interestService;
        private readonly CurrentUser currentUser;
        private readonly MediaService mediaService;
        private readonly AlbumService albumService;
        private readonly FriendshipService friendshipService;
        private readonly PostService postService;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IImageStorage imageStorage;

        public ProfileController(IHostingEnvironment hostingEnvironment, PostService postService, CurrentUser currentUser, 
            FriendshipService friendshipService, AlbumService albumService, MediaService mediaService, ProfileService profileService, 
            CountyService countyService, CityService cityService, InterestService interestService, IMapper mapper,
            UserManager<User> userManager, SignInManager<User> signInManager, IImageStorage imageStorage)
            : base(mapper)
        {
            this.imageStorage = imageStorage;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.friendshipService = friendshipService;
            this.albumService = albumService;
            this.mediaService = mediaService;
            this.hostingEnvironment = hostingEnvironment;
            this.currentUser = currentUser;
            this.profileService = profileService;
            this.countyService = countyService;
            this.cityService = cityService;
            this.interestService = interestService;
            this.postService = postService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string id)
        {
            ViewProfileVM model = null;
            var currentUser = await userManager.GetUserAsync(User);

            if (id == null)
            {
                id = currentUser.Id;
            }

            model = profileService.GetViewProfileVM(id);
            model.FilePath = imageStorage.UriFor(model.FilePath);

            if (model == null)
            {
                return UserNotFoundView();
            }

            if (id != currentUser.Id)
            {
                model.MutualFriends = friendshipService.CountMutualFriends(id);
            }

            model.Interests = interestService.GetAll();
            model.RelationToCurrentUser = profileService.GetRelationToCurrentUser(currentUser.Id, id);

            if (model.IsPrivate && model.RelationToCurrentUser == RelationTypes.Strangers && await userManager.IsInRoleAsync(currentUser, "Administrator") == false)
            {
                model.Albums = null;
                model.Interests = null;
                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ProfileVM model = null;
            var currentUser = await userManager.GetUserAsync(User);

            if (id == null)
            {
                id = currentUser.Id;
            }
            else
            {
                if (id == currentUser.Id)
                {
                    model = profileService.GetEditProfileVM(id);
                }
                else
                {
                    if (await userManager.IsInRoleAsync(currentUser, "Administrator"))
                    {
                        model = profileService.GetEditProfileVM(id);
                    }
                    else
                    {
                        return ForbidView();
                    }
                }
            }

            if (model == null)
            {
                return UserNotFoundView();
            }

            model.Counties = countyService.GetSelectCounties();
            model.Cities = cityService.GetCities(model.CountyId);
            model.Interests = interestService.GetAll();
            model.MyInterests = interestService.GetByUser(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Counties = countyService.GetSelectCounties();
                model.Cities = cityService.GetCities(model.CountyId);
                model.Interests = interestService.GetAll();
                model.MyInterests = interestService.GetByUser(model.Id);

                return View(model);
            }

            var currentUser = await userManager.GetUserAsync(User);
            var result = profileService.UpdateUser(model);
            var file = model.ProfilePhoto;

            if (file != null && result)
            {
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, @"images\uploads");
                var type = file.ContentType.ToString().Split('/');

                if (file.Length > 0)
                {
                    if (type[0] == "image")
                    {
                        var albumId = albumService.GetId("Profile Pictures", model.Id);
                        if (albumId == null)
                        {
                            albumId = albumService.Create("Profile Pictures", model.Id);
                        }

                        try
                        {
                            var imageId = await imageStorage.SaveImage(file.OpenReadStream(), type[1]);
                            var photo = mediaService.Add(albumId, imageId, MediaTypes.Image);

                            if (photo == null)
                            {
                                return InternalServerErrorView();
                            }

                            var hasModified = profileService.ChangeProfilePhoto(imageId, model.Id);

                            if (hasModified)
                            {
                                postService.NotifyProfilePhotoChanged(photo, model.Id);
                            }

                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
            }

            if (!result)
            {
                return InternalServerErrorView();
            }

            if (await userManager.IsInRoleAsync(currentUser, "Administrator") && model.Id != currentUser.Id)
            {
                return RedirectToAction("Index", "User");
            }

            return RedirectToAction("Get", "Profile", new { id = currentUser.Id });
        }

        //[HttpGet]
        //[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        //[AllowAnonymous]
        //public string RenderProfilePicture(string id)
        //{
        //    var media = profileService.GetUserPhoto(id);

        //    if (media == null)
        //    {
        //        return imageStorage.UriFor("no-profile.png");
        //    }

        //    return imageStorage.UriFor(media);
        //}
    }
}