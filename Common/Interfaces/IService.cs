namespace Common.Interfaces;

public interface IService<TEntity>
    where TEntity : IEntity, new()
{

}
