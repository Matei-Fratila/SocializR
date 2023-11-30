﻿namespace SocializR.DataAccess.Base;

public class BaseRepository<TEntity>(SocializRContext context) : IRepository<TEntity>
        where TEntity : class, IEntity, new()
{
    protected SocializRContext Context { get; } = context;

    public IQueryable<TEntity> Query { get; } = context.Set<TEntity>();

    public virtual void Add(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().AddRange(entities);
    }

    public virtual void Update(TEntity entity)
    {
        Context.Update<TEntity>(entity);
    }

    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().UpdateRange(entities);
    }

    public virtual void Remove(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().RemoveRange(entities);
    }
}