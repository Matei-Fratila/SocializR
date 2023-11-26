using SocializR.DataAccess.UnitOfWork;
using SocializR.Entities;
using SocializR.Services.Base;
using SocializR.Services.CommentServices;
using SocializR.Services.LikeServices;
using SocializR.Services.PostServices;
using System;
using SocializR.Entities.DTOs.Common;
using SocializR.Entities.DTOs.Feed;
using System.Collections.Generic;

namespace SocializR.Services.FeedServices
{
    public class FeedService : BaseService
    {
        private readonly PostService postService;
        private readonly CommentService commentService;
        private readonly LikeService likeService;
        private readonly CurrentUser currentUser;

        public FeedService(PostService postService, CommentService commentService, LikeService likeService, SocializRUnitOfWork unitOfWork,
            CurrentUser currentUser)
            : base(unitOfWork)
        {
            this.currentUser = currentUser;
            this.postService = postService;
            this.commentService = commentService;
            this.likeService = likeService;
        }

        public bool AddPost(string userId, string title, string body, List<Media> media)
        {
            var newPost = new Post
            {
                UserId = userId,
                Title = title,
                Body = body,
                CreatedOn = DateTime.Now,
            };

            if (media != null)
            {
                newPost.Media = media;
            }

            return postService.AddPost(newPost);
        }

        public bool DeletePost(string postId)
        {
            return postService.DeletePost(postId);
        }

        public string AddComment(string currentUserId, string body, string postId)
        {
            var comment = new Comment
            {
                UserId = currentUserId,
                Body = body,
                CreatedOn = DateTime.Now,
                PostId = postId
            };

            return commentService.AddComment(comment);
        }

        public bool DeleteComment(string commentId)
        {
            return commentService.DeleteComment(commentId);
        }

        public void MarkPosts(FeedVM feed)
        {
            foreach(var post in feed.Posts)
            {
                var isLiked = post.Likes.Find(l => l.UserId == currentUser.Id);

                post.IsCurrentUserPost = post.UserId == currentUser.Id ? true : false;

                post.IsLikedByCurrentUser = isLiked != null ? true : false;

                foreach(var comment in post.Comments)
                {
                    comment.IsCurrentUserComment = comment.UserId == currentUser.Id ? true : false;
                }
            }
        }

        public FeedVM GetNextPosts(int page, int postsPerPage, int commentsPerPage)
        {
            var posts = postService.GetNextPosts(currentUser.Id, page, postsPerPage, commentsPerPage);

            var feed = new FeedVM
            {
                Posts = posts
            };

            MarkPosts(feed);

            return feed;
   
        }

        public bool DeleteLike(string currentUserId, string id)
        {
            return likeService.DeleteLike(currentUserId, id);
        }

        public bool LikePost(string currentUserId, string id)
        {
            return likeService.AddLike(currentUserId, id);
        }

        public CommentVM CurrentUserComment(string body)
        {
            return commentService.CurrentUserComment(body);
        }
    }
}
