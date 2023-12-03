namespace SocializR.Services.Interfaces;
public interface IBaseService<TEntity> where TEntity : IEntity, new()
{
    IQueryable<TEntity> Query { get; }
    IEnumerable<TEntity> GetAll();
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void Remove(Guid id);
    void RemoveRange(IEnumerable<TEntity> entities);
    void UpdateRange(IEnumerable<TEntity> entities);
}
