using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocializR.DataAccess.Base
{
    public class BaseUnitOfWork : IUnitOfWork, IDisposable
    {
        protected readonly SocializRContext DbContext;

        public BaseUnitOfWork(SocializRContext context)
        {
            this.DbContext = context;
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
