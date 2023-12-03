using System.Reflection;

namespace SocializR.Services.Base;

public abstract class BaseService<TEntity, TService>(ApplicationUnitOfWork unitOfWork) : IDisposable, IBaseService<TEntity>
    where TEntity : IEntity, new()
    where TService : IBaseService<TEntity>
{
    protected readonly ApplicationUnitOfWork UnitOfWork = unitOfWork;
    protected readonly List<PropertyInfo> Repo0 = unitOfWork.GetType().GetProperties().ToList();
    protected readonly IBaseRepository<TEntity> Repo
        = unitOfWork.GetType()
        .GetProperties()
        .First(p => p.PropertyType.FullName == typeof(IBaseRepository<TEntity>).FullName)
        .GetValue(unitOfWork) as IBaseRepository<TEntity>;

    public IQueryable<TEntity> Query
    {
        get
        {
            return Repo.Query;
        }
    }

    public TEntity Get(Guid id)
        => Repo.Get(id);

    public IEnumerable<TEntity> GetAll()
        => Repo.Query.ToList();

    public void Add(TEntity entity)
        => Repo.Add(entity);

    public void AddRange(IEnumerable<TEntity> entities)
        => Repo.AddRange(entities);

    public void Remove(TEntity entity)
        => Repo.Remove(entity);

    public void Remove(Guid id)
    {
        var entity = Repo.Get(id);
        Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
        => Repo.RemoveRange(entities);

    public void Update(TEntity entity)
        => Repo.Update(entity);

    public void UpdateRange(IEnumerable<TEntity> entities)
        => Repo.UpdateRange(entities);

    public void Dispose()
    {
        UnitOfWork.Dispose();
    }
}
