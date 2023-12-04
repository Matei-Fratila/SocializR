namespace SocializR.DataAccess.Base;

public class BaseUnitOfWork(ApplicationDbContext context) : IUnitOfWork, IDisposable
{
    protected readonly ApplicationDbContext DbContext = context;

    public int SaveChanges()
    {
        return DbContext.SaveChanges();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await DbContext.SaveChangesAsync() != 0;
    }

    public void Dispose()
    {
        DbContext.Dispose();
    }
}
