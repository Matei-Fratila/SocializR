using Microsoft.AspNetCore.Identity;
using SocializR.DataAccess.UnitOfWork;
using SocializR.Entities;
using SocializR.Entities.DTOs.Common;
using SocializR.Services.Base;
using System.Linq;
using System.Transactions;

namespace SocializR.Services.LikeServices
{
    public class LikeService : BaseService
    {
        public LikeService(CurrentUser currentUser, SocializRUnitOfWork unitOfWork, UserManager<User> userManager)
            : base(unitOfWork)
        {
        }

        public bool AddLike(string currentUserId, string postId)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var like = UnitOfWork.Likes.Query
                    .Where(l => l.PostId == postId && l.UserId == currentUserId)
                    .FirstOrDefault();

                if (like != null)
                {
                    return false;
                }

                like = new Like
                {
                    PostId = postId,
                    UserId = currentUserId
                };

                UnitOfWork.Likes.Add(like);

                var sucess = UnitOfWork.SaveChanges() != 0;

                scope.Complete();

                return sucess;
            }
        }

        public bool DeleteLike(string currentUserId, string postId)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var like = UnitOfWork.Likes.Query
                    .Where(l => l.PostId == postId && l.UserId == currentUserId)
                    .FirstOrDefault();

                if (like != null)
                {
                    UnitOfWork.Likes.Remove(like);
                }

                var sucess = UnitOfWork.SaveChanges() != 0;

                scope.Complete();

                return sucess;
            }
        }
    }
}
