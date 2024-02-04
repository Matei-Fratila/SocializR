using Socializr.Models.Entities.Base;

namespace SocializR.DataAccess.Base;

public class BaseRepository<TEntity>(ApplicationDbContext context) : IBaseRepository<TEntity>
        where TEntity : class, IEntity, new()
{
    protected ApplicationDbContext Context { get; } = context;

    public IQueryable<TEntity> Query { get; } = context.Set<TEntity>();

    public virtual TEntity Get(Guid id)
    {
        return Context.Set<TEntity>().Find(id);
    }

    public virtual async Task<TEntity> GetAsync(Guid id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public virtual void Add(TEntity entity)
    {
        entity.CreatedDate = DateTime.Now;
        Context.Set<TEntity>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities) { 
            entity.CreatedDate = DateTime.Now; 
        }

        Context.Set<TEntity>().AddRange(entities);
    }

    public virtual void Update(TEntity entity)
    {
        entity.LastModifiedDate = DateTime.Now;
        Context.Update<TEntity>(entity);
    }

    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.LastModifiedDate = DateTime.Now;
        }

        Context.Set<TEntity>().UpdateRange(entities);
    }

    public virtual void Remove(TEntity entity)
    {
        entity.IsDeleted = true;
        entity.LastModifiedDate = DateTime.Now;
        Context.Update<TEntity>(entity);
    }

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.IsDeleted = true;
            entity.LastModifiedDate = DateTime.Now;
        }

        Context.Set<TEntity>().RemoveRange(entities);
    }
}