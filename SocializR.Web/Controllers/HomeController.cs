using Common.Interfaces;
using System.Web;

namespace SocializR.Web.Controllers;

[Authorize]
public class HomeController(IOptionsMonitor<AppSettings> _configuration,
    IMapper _mapper,
    AlbumService _albumService,
    FeedService _feedService,
    MediaService _mediaService,
    IHostEnvironment _hostingEnvironment,
    UserManager<User> _userManager,
    CommentService _commentService,
    IImageStorage _imageStorage) : BaseController(_mapper)
{
    [HttpGet]
    public IActionResult Index()
    {
        var model = _feedService.GetNextPosts(0, _configuration.CurrentValue.PostsPerPage, _configuration.CurrentValue.CommentsPerPage);

        foreach (var comment in model.Posts.SelectMany(p => p.Comments))
        {
            comment.UserPhoto = _imageStorage.UriFor(comment.UserPhoto ?? _configuration.CurrentValue.DefaultProfilePicture);
        }

        return View(model);
    }

    [HttpGet]
    public JsonResult NextPosts(int page)
    {
        var model = _feedService.GetNextPosts(page, _configuration.CurrentValue.PostsPerPage, _configuration.CurrentValue.PostsPerPage);

        foreach (var comment in model.Posts.SelectMany(p => p.Comments))
        {
            comment.UserPhoto = _imageStorage.UriFor(comment.UserPhoto ?? _configuration.CurrentValue.DefaultProfilePicture);
        }

        return Json(model);
    }

    [HttpGet]
    public JsonResult NextComments(int page, Guid postId)
    {
        var comments = _commentService.GetComments(postId, _configuration.CurrentValue.CommentsPerPage, page);

        foreach (var comment in comments)
        {
            comment.UserPhoto = _imageStorage.UriFor(comment.UserPhoto ?? _configuration.CurrentValue.DefaultProfilePicture);
        }

        return Json(comments);
    }

    [HttpPost]
    public async Task<IActionResult> AddPost(AddPostViewModel model)
    {
        var media = new List<Media>();
        var uploads = Path.Combine(_hostingEnvironment.ContentRootPath, _configuration.CurrentValue.FileUploadLocation);
        if (model.Media != null)
        {
            foreach (var file in model.Media)
            {
                var id = _albumService.GetPostsAlbum();

                if (file.Length > 0)
                {
                    var type = file.ContentType.ToString().Split('/');
                    if (type[0] == "image" || type[0] == "video")
                    {
                        var filePath = Path.Combine(uploads, Path.GetRandomFileName() + '.' + type[1]);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        media.Add(_mediaService.Add(id, Path.GetRelativePath("wwwroot", filePath), type[0] == "image" ? MediaTypes.Image : MediaTypes.Video));
                    }
                }
            }
        }

        var result = _feedService.AddPost(_userManager.GetUserId(User), model.Title, model.Body, media);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return Ok();

    }

    [HttpPost]
    public IActionResult DeletePost(string postId)
    {
        var result = _feedService.DeletePost(postId);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpPost]
    public IActionResult Like(string id)
    {
        var result = _feedService.LikePost(_userManager.GetUserId(User), id);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpPost]
    public JsonResult AddComment([FromBody] AddCommentViewModel comment)
    {
        var id = _feedService.AddComment(_userManager.GetUserId(User), HttpUtility.HtmlEncode(comment.Body), comment.PostId);

        return Json(new { id });
    }

    [HttpPost]
    public IActionResult DeleteComment(string commentId)
    {
        var result = _feedService.DeleteComment(commentId);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpPost]
    public IActionResult DeleteLike(string id)
    {
        var result = _feedService.DeleteLike(_userManager.GetUserId(User), id);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpGet]
    public IActionResult GetCommentWidget(string body)
    {
        var comment = _feedService.CurrentUserComment(body);

        return PartialView("Views/Home/_Comment.cshtml", comment);
    }

    [HttpGet]
    public IActionResult GetComment(CommentViewModel comment)
    {

        return PartialView("Views/Home/_Comment.cshtml", comment);
    }

    [HttpGet]
    public IActionResult GetPostWidget(PostVM model)
    {
        return PartialView("Views/Home/_Post.cshtml", model);
    }

    //[HttpGet]
    //public IActionResult GetMediaWidget(MediaModel model)
    //{
    //    return PartialView("Views/Home/_Media.cshtml", model);
    //}

    [HttpGet]
    public IActionResult GetImageWidget(MediaViewModel model)
    {
        return PartialView("Views/Home/_Image.cshtml", model);
    }

    [HttpGet]
    public IActionResult GetVideoWidget(MediaViewModel model)
    {
        return PartialView("Views/Home/_Video.cshtml", model);
    }

    public IActionResult About()
    {
        ViewData["Message"] = "Your application description page.";

        return View();
    }

    public IActionResult Contact()
    {
        ViewData["Message"] = "Your contact page.";

        return View();
    }

    [AllowAnonymous]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
