namespace SocializR.Web.Controllers;

public class HomeController(IMapper _mapper,
    ApplicationUnitOfWork _applicationUnitOfWork,
    IPostService _postService,
    ICommentService _commentService,
    ILikeService _likeService,
    CurrentUser _currentUser,
    IImageStorage _imageStorage) : BaseController(_mapper)
{

    [HttpGet]
    public async Task<IActionResult> IndexAsync()
    {
        var model = await _postService.GetPaginatedAsync(_currentUser.Id, 0, isProfileView: false);

        return View(model);
    }

    [HttpGet]
    public async Task<JsonResult> NextPostsAsync(Guid userId, int page, bool isProfileView)
    {
        if(userId == Guid.Empty)
        {
            userId = _currentUser.Id;
        }

        var model = await _postService.GetPaginatedAsync(userId, page, isProfileView: isProfileView);

        return Json(model);
    }

    [HttpGet]
    public async Task<JsonResult> NextCommentsAsync(int page, Guid postId)
    {
        var comments = await _commentService.GetPaginatedAsync(postId, _currentUser.Id, page);

        return Json(comments);
    }

    [HttpPost]
    public async Task<IActionResult> AddPostAsync(AddPostViewModel model)
    {
        await _postService.AddAsync(model);

        if (!await _applicationUnitOfWork.SaveChangesAsync())
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> DeletePostAsync(Guid id)
    {
        _postService.Remove(id);

        if (!await _applicationUnitOfWork.SaveChangesAsync())
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
    public async Task<Guid> AddCommentAsync([FromBody] AddCommentViewModel comment)
    {
        var newComment = new Comment();
        _mapper.Map(comment, newComment);
        newComment.UserId = _currentUser.Id;

        _commentService.Add(newComment);
        await _applicationUnitOfWork.SaveChangesAsync();

        return newComment.Id;
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCommentAsync(Guid id)
    {
        _commentService.Remove(id);

        if (!await _applicationUnitOfWork.SaveChangesAsync())
        {
            return InternalServerErrorView();
        }

        return Ok();
    }

    [HttpGet]
    public IActionResult GetCommentWidget(string body)
    {
        var comment = _commentService.GetCurrentUserCommentViewModel(body);

        //comment.UserPhoto = _imageStorage.UriFor(comment.UserPhoto ?? _defaultProfilePicture);

        return PartialView("_Comment", comment);
    }

    [HttpGet]
    public IActionResult GetPostWidget(PostVM model)
    {
        return PartialView("_Post", model);
    }

    [HttpGet]
    public IActionResult GetMediaWidget(MediaViewModel model)
    {
        return PartialView("_Media", model);
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
