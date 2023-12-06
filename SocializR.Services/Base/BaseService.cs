namespace SocializR.Services.Base;

public abstract class BaseService<TEntity, TService>(ApplicationUnitOfWork unitOfWork) : IDisposable, IBaseService<TEntity>
    where TEntity : IEntity, new()
    where TService : IBaseService<TEntity>
{
    protected readonly ApplicationUnitOfWork UnitOfWork = unitOfWork;
    protected readonly IBaseRepository<TEntity> Repository
        = unitOfWork.GetType()
        .GetProperties()
        .First(p => p.PropertyType.FullName == typeof(IBaseRepository<TEntity>).FullName)
        .GetValue(unitOfWork) as IBaseRepository<TEntity>;

    public IQueryable<TEntity> Query
    {
        get
        {
            return Repository.Query;
        }
    }

    public TEntity Get(Guid id)
        => Repository.Get(id);

    public async Task<TEntity> GetAsync(Guid id)
        => await Repository.GetAsync(id);

    public IEnumerable<TEntity> GetAll()
        => Repository.Query.ToList();

    public void Add(TEntity entity)
        => Repository.Add(entity);

    public void AddRange(IEnumerable<TEntity> entities)
        => Repository.AddRange(entities);

    public void Remove(TEntity entity)
        => Repository.Remove(entity);

    public void Remove(Guid id)
    {
        var entity = Repository.Get(id);
        Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
        => Repository.RemoveRange(entities);

    public void Update(TEntity entity)
        => Repository.Update(entity);

    public void UpdateRange(IEnumerable<TEntity> entities)
        => Repository.UpdateRange(entities);

    public void Dispose()
    {
        UnitOfWork.Dispose();
    }
}
