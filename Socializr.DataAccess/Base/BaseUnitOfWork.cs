namespace SocializR.DataAccess.Base;

public class BaseUnitOfWork(ApplicationDbContext context) : IUnitOfWork, IDisposable
{
    protected readonly ApplicationDbContext DbContext = context;

    public int SaveChanges()
    {
        return DbContext.SaveChanges();
    }

    public void Dispose()
    {
        DbContext.Dispose();
    }
}
