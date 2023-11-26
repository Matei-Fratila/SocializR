using SocializR.Web.Code.Base;
using SocializR.Services.AlbumServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocializR.Entities.DTOs.Album;
using Microsoft.AspNetCore.Authorization;
using SocializR.Services.MediaServices;
using Common.Interfaces;
using SocializR.Entities.Enums;
using System.Diagnostics;
using System.IO;

namespace SocializR.Web.Controllers
{
    [Authorize]
    public class AlbumController : BaseController
    {
        private readonly AlbumService albumService;
        private readonly MediaService mediaService;
        private readonly IValidationService validationService;
        private readonly IImageStorage imageStorage;

        public AlbumController(AlbumService albumService, MediaService mediaService, IMapper mapper, IValidationService validationService,
            IImageStorage imageStorage)
            : base(mapper)
        {
            this.imageStorage = imageStorage;
            this.validationService = validationService;
            this.mediaService = mediaService;
            this.albumService = albumService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new AlbumsVM
            {
                Albums = albumService.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateAlbumVM model)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("Index", "Album");
            }

            var result = albumService.Add(model);

            if (!result)
            {
                return InternalServerErrorView();
            }

            return RedirectToAction("Index", "Album");
        }

        [HttpPost]
        public IActionResult Edit(CreateAlbumVM model)
        {
            if (ModelState.IsValid)
            {
                var result = albumService.Update(model);

                if (!result)
                {
                    return RedirectToAction("Index", "Album");
                }
            }

            return RedirectToAction("Index", "Album");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var isSuccessStatusCode = albumService.Delete(id);

            return RedirectToAction("Index", "Album");
        }

        [HttpGet]
        [AllowAnonymous]
        public string RenderCoverPicture(string id)
        {
            if (id == null)
            {
                return imageStorage.UriFor("default-image.jpg");
            }

            var media = mediaService.Get(id);

            if (media == null) 
            {
                return imageStorage.UriFor("default-image.jpg");
            }

            return imageStorage.UriFor(media);

        }
    }
}
