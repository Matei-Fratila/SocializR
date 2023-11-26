using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SocializR.DataAccess.UnitOfWork;
using SocializR.Entities;
using SocializR.Entities.DTOs.Common;
using SocializR.Services.Base;
using System;
using System.Linq;

namespace SocializR.Services.UserServices
{
    public class AccountService : BaseService
    {
        private readonly IMapper mapper;

        public AccountService(SocializRUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork)
        {
            this.mapper = mapper;
        }

        public CurrentUser Get(string email)
        {
            return UnitOfWork.Users.Query
                    .Where(u=>u.Email==email)
                    .ProjectTo<CurrentUser>(mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        //TODO: add users in PublicUser role
        public bool Register(User user)
        {
            user.IsActive = true;
            user.IsDeleted = false;
            user.CreatedOn = DateTime.Now;

            UnitOfWork.Users.Add(user);

            return UnitOfWork.SaveChanges() != 0;
        }
    }
}
