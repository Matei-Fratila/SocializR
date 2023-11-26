namespace SocializR.Web.Controllers;

//[Authorize]
public class HomeController : BaseController
{
    private readonly UserManager<User> userManager;
    private readonly HtmlEncoder htmlEncoder;
    private readonly FeedService feedService;
    private readonly AppSettings configuration;
    private readonly MediaService mediaService;
    private readonly AlbumService albumService;
    private readonly CommentService commentService;
    private readonly IHostEnvironment hostingEnvironment;

    public HomeController(IOptions<AppSettings> configuration, IMapper mapper, AlbumService albumService, FeedService feedService, 
        MediaService mediaService, IHostEnvironment hostingEnvironment, UserManager<User> userManager, HtmlEncoder htmlEncoder,
        CommentService commentService) :
        base(mapper)
    {
        this.htmlEncoder = htmlEncoder;
        this.userManager = userManager;
        this.albumService = albumService;
        this.commentService = commentService;
        this.mediaService = mediaService;
        this.hostingEnvironment = hostingEnvironment;
        this.configuration = configuration.Value;
        this.feedService = feedService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var model = feedService.GetNextPosts(0, configuration.PostsPerPage, configuration.CommentsPerPage);

        return View(model);
    }

    [HttpGet]
    public JsonResult NextPosts(int page)
    {
        var posts = feedService.GetNextPosts(page, configuration.PostsPerPage, configuration.PostsPerPage);

        return Json(posts);
    }

    [HttpGet]
    public JsonResult NextComments(int page, string postId)
    {
        var comments = commentService.GetComments(postId, configuration.CommentsPerPage, page);

        return Json(comments);
    }

    [HttpPost]
    public async Task<IActionResult> AddPost(AddPostModel model)
    {
        var media = new List<Media>();
        var uploads = Path.Combine(hostingEnvironment.ContentRootPath, @"images\uploads");
        if (model.Media != null)
        {
            foreach (var file in model.Media)
            {
                var id = albumService.GetPostsAlbum();

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

                        media.Add(mediaService.Add(id, Path.GetRelativePath("wwwroot", filePath), type[0] == "image" ? MediaTypes.Image : MediaTypes.Video));
                    }
                }
            }
        }

        var result = feedService.AddPost(userManager.GetUserId(User), htmlEncoder.Encode(model.Title), htmlEncoder.Encode(model.Body), media);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return Ok();

    }

    [HttpPost]
    public IActionResult DeletePost(string postId)
    {
        var result = feedService.DeletePost(postId);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpPost]
    public IActionResult Like(string id)
    {
        var result = feedService.LikePost(userManager.GetUserId(User), id);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpPost]
    public JsonResult AddComment([FromBody]AddCommentModel comment)
    {
        var id = feedService.AddComment(userManager.GetUserId(User), htmlEncoder.Encode(comment.Body), comment.PostId);

        return Json(new { id });
    }

    [HttpPost]
    public IActionResult DeleteComment(string commentId)
    {
        var result = feedService.DeleteComment(commentId);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpPost]
    public IActionResult DeleteLike(string id)
    {
        var result = feedService.DeleteLike(userManager.GetUserId(User), id);

        if (!result)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpGet]
    public IActionResult GetCommentWidget(string body)
    {
        var comment = feedService.CurrentUserComment(body);

        return PartialView("Views/Home/_Comment.cshtml", comment);
    }

    [HttpGet]
    public IActionResult GetComment(CommentVM comment)
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
    public IActionResult GetImageWidget(MediaModel model)
    {
        return PartialView("Views/Home/_Image.cshtml", model);
    }

    [HttpGet]
    public IActionResult GetVideoWidget(MediaModel model)
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
