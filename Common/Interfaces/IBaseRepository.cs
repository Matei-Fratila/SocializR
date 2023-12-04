using System;

namespace Common.Interfaces;

public interface IBaseRepository<TEntity> : IRepository
    where TEntity : IEntity
{
    IQueryable<TEntity> Query { get; }
    TEntity Get(Guid id);
    Task<TEntity> GetAsync(Guid id);
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    void UpdateRange(IEnumerable<TEntity> entities);
}

public interface IRepository
{

}
