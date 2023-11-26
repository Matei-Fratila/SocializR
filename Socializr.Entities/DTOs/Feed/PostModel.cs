using SocializR.Entities.DTOs.Media;
using System;
using System.Collections.Generic;

namespace SocializR.Entities.DTOs.Feed
{
    public class PostModel
    {
        public bool IsLikedByCurrentUser { get; set; }
        public bool IsCurrentUserPost { get; set; }

        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserPhoto { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public List<MediaModel> Media { get; set; }
        public DateTime CreatedOn { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfComments { get; set; }

        public List<CommentVM> Comments { get; set; }
        public List<LikeVM> Likes { get; set; }
    }
}
