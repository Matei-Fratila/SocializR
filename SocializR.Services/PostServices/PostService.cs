using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SocializR.DataAccess.UnitOfWork;
using SocializR.Entities.DTOs.Feed;
using SocializR.Entities;
using SocializR.Entities.DTOs.Common;
using SocializR.Services.Base;
using System.Collections.Generic;
using System.Linq;
using System;
using SocializR.Services.CommentServices;

namespace SocializR.Services.PostServices
{
    public class PostService : BaseService
    {
        private readonly CurrentUser currentUser;
        private readonly CommentService commentService;
        private readonly IMapper mapper;

        public PostService(CurrentUser currentUser, SocializRUnitOfWork unitOfWork, IMapper mapper, CommentService commentService)
            : base(unitOfWork)
        {
            this.mapper = mapper;
            this.currentUser = currentUser;
            this.commentService = commentService;
        }

        public List<PostVM> GetNextPosts(string currentUserId, int page, int postsPerPage, int commentsPerPage)
        {
            var posts = UnitOfWork.Posts.Query
                .Where(p => p.User.FriendsFirstUser.FirstOrDefault(f => f.SecondUserId == currentUser.Id && f.FirstUser.IsDeleted==false) != null || p.UserId == currentUserId)
                .OrderByDescending(p => p.CreatedOn)
                .Skip(page * postsPerPage)
                .Take(postsPerPage)
                .ProjectTo<PostVM>(mapper.ConfigurationProvider)
                .ToList();

            foreach(var post in posts)
            {
                post.Comments = commentService.GetComments(post.Id, commentsPerPage, 0);
            }

            return posts;
        }

        public bool AddPost(Post post)
        {
            UnitOfWork.Posts.Add(post);

            return UnitOfWork.SaveChanges()!=0;
        }

        public bool NotifyProfilePhotoChanged(Media photo, string userId)
        {
            var post = new Post
            {
                UserId = userId,
                Title = "added a new profile photo",
                Body="",
                CreatedOn = DateTime.Now,
                Media=new List<Media>
                {
                    photo
                }
            };

            UnitOfWork.Posts.Add(post);

            return UnitOfWork.SaveChanges() != 0;
        }

        public bool DeletePost(string postId)
        {
            //todod fix npe
            var post = UnitOfWork.Posts.Query
                .Include(c => c.Likes)
                .Include(c => c.Comments)
                .Include(c => c.Media)
                .Where(p => p.Id == postId)
                .FirstOrDefault();

            if (post == null)
            {
                return false;
            }

            UnitOfWork.Posts.Remove(post);

            return UnitOfWork.SaveChanges() != 0;
        }
    }
}
