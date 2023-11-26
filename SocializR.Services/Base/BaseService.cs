using SocializR.DataAccess.UnitOfWork;
using System;

namespace SocializR.Services.Base
{
    public class BaseService : IDisposable
    {
        protected readonly SocializRUnitOfWork UnitOfWork;

        public BaseService(SocializRUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
