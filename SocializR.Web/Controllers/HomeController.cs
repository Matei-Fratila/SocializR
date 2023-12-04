﻿namespace SocializR.Web.Controllers;

[Authorize]
public class HomeController(IMapper _mapper,
    ApplicationUnitOfWork _applicationUnitOfWork,
    IPostService _postService,
    ICommentService _commentService,
    ILikeService _likeService,
    IOptionsMonitor<AppSettings> _appSettings,
    CurrentUser _currentUser,
    IImageStorage _imageStorage) : BaseController(_mapper)
{
    private readonly int _postsPerPage = _appSettings.CurrentValue.PostsPerPage;
    private readonly int _commentsPerFirstPage = _appSettings.CurrentValue.CommentsPerFirstPage;
    private readonly int _commentsPerPage = _appSettings.CurrentValue.CommentsPerPage;
    private readonly string _defaultProfilePicture = _appSettings.CurrentValue.DefaultProfilePicture;
    private readonly string _defaultPostsAlbumName = _appSettings.CurrentValue.PostsAlbumName;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = await _postService.GetPaginatedAsync(_currentUser.Id, 0, _postsPerPage, _commentsPerFirstPage, _defaultProfilePicture);

        return View(model);
    }

    [HttpGet]
    public async Task<JsonResult> NextPosts(int page)
    {
        var model = await _postService.GetPaginatedAsync(_currentUser.Id, page, _postsPerPage, _commentsPerPage, _defaultProfilePicture);

        return Json(model);
    }

    [HttpGet]
    public async Task<JsonResult> NextComments(int page, Guid postId)
    {
        var comments = await _commentService.GetPaginatedAsync(postId, _commentsPerPage, page, _defaultProfilePicture);

        return Json(comments);
    }

    [HttpPost]
    public async Task<IActionResult> AddPost(AddPostViewModel model)
    {
        await _postService.AddAsync(model, _defaultPostsAlbumName);

        if (!await _applicationUnitOfWork.SaveChangesAsync())
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpPost]
    public IActionResult DeletePost(Guid id)
    {
        _postService.Remove(id);

        if (_applicationUnitOfWork.SaveChanges() == 0)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> LikeAsync(Guid id)
    {
        await _likeService.AddLikeAsync(id);

        if (_applicationUnitOfWork.SaveChanges() == 0)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteLikeAsync(Guid id)
    {
        await _likeService.DeleteLikeAsync(id);

        if (_applicationUnitOfWork.SaveChanges() == 0)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpPost]
    public Guid AddComment([FromBody] AddCommentViewModel comment)
    {
        var newComment = new Comment
        {
            UserId = _currentUser.Id,
            Body = comment.Body,
            CreatedOn = DateTime.Now,
            PostId = comment.PostId
        };

        _commentService.Add(newComment);
        _applicationUnitOfWork.SaveChanges();

        return newComment.Id;
    }

    [HttpPost]
    public IActionResult DeleteComment(Guid id)
    {
        _commentService.Remove(id);

        if (_applicationUnitOfWork.SaveChanges() == 0)
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpGet]
    public IActionResult GetCommentWidget(string body)
    {
        var comment = _commentService.GetCurrentUserCommentViewModel(body);

        comment.UserPhoto = _imageStorage.UriFor(comment.UserPhoto ?? _defaultProfilePicture);

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
