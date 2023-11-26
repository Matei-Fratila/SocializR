namespace SocializR.Services.Base;

public class BaseService : IDisposable
{
    protected readonly SocializRUnitOfWork UnitOfWork;

    public BaseService(SocializRUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public void Dispose()
    {
        UnitOfWork.Dispose();
    }
}
