using Common.Interfaces;
using SocializR.DataAccess.UnitOfWork;
using SocializR.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocializR.Services.ValidationService
{
    public class ValidationService : BaseService, IValidationService 
    {
        public ValidationService(SocializRUnitOfWork unitOfWork)
            :base(unitOfWork)
        {

        }

        public bool EmailExists(string email)
        {
            return UnitOfWork.Users.Query
                .Any(u => u.Email == email);
        }

        public bool AlbumExists(string name, string id)
        {
            return UnitOfWork.Albums.Query
                .Any(a => a.Name == name && a.Id != id);
        }
    }
}
