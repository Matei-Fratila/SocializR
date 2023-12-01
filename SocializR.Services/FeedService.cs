namespace SocializR.Services;

public class FeedService(PostService _postService, 
    CommentService _commentService, 
    LikeService _likeService, 
    SocializRUnitOfWork unitOfWork,
    CurrentUser _currentUser) : BaseService(unitOfWork)
{
    public bool AddPost(string userId, string title, string body, List<Media> media)
    {
        var newPost = new Post
        {
            UserId = new Guid(userId),
            Title = title,
            Body = body,
            CreatedOn = DateTime.Now,
        };

        if (media != null)
        {
            newPost.Media = media;
        }

        return _postService.AddPost(newPost);
    }

    public bool DeletePost(string postId)
    {
        return _postService.DeletePost(postId);
    }

    public string AddComment(string currentUserId, string body, string postId)
    {
        var comment = new Comment
        {
            UserId = new Guid(currentUserId),
            Body = body,
            CreatedOn = DateTime.Now,
            PostId = new Guid(postId)
        };

        return _commentService.AddComment(comment);
    }

    public bool DeleteComment(string commentId)
    {
        return _commentService.DeleteComment(commentId);
    }

    public void MarkPosts(FeedViewModel feed)
    {
        foreach (var post in feed.Posts)
        {
            var isLiked = post.Likes.Find(l => l.UserId == _currentUser.Id);

            post.IsCurrentUserPost = post.UserId == _currentUser.Id ? true : false;

            post.IsLikedByCurrentUser = isLiked != null ? true : false;

            foreach (var comment in post.Comments)
            {
                comment.IsCurrentUserComment = comment.UserId == _currentUser.Id ? true : false;
            }
        }
    }

    public FeedViewModel GetNextPosts(int page, int postsPerPage, int commentsPerPage)
    {
        var posts = _postService.GetNextPosts(_currentUser.Id, page, postsPerPage, commentsPerPage);

        var feed = new FeedViewModel
        {
            Posts = posts
        };

        MarkPosts(feed);

        return feed;

    }

    public bool DeleteLike(string currentUserId, string id)
    {
        return _likeService.DeleteLike(currentUserId, id);
    }

    public bool LikePost(string currentUserId, string id)
    {
        return _likeService.AddLike(currentUserId, id);
    }

    public CommentViewModel CurrentUserComment(string body)
    {
        return _commentService.CurrentUserComment(body);
    }
}
