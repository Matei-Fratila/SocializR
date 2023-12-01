namespace SocializR.Services.Base;

public class BaseService(SocializRUnitOfWork unitOfWork) : IDisposable
{
    protected readonly SocializRUnitOfWork UnitOfWork = unitOfWork;

    public void Dispose()
    {
        UnitOfWork.Dispose();
    }
}
