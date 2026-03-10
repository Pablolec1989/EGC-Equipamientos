using EGC.Domain.Abstractions;

namespace EGC.Domain.Repositories
{
    public interface IRepositoryBase<TEntity, TEntityId>
        where TEntity : EntityBase<TEntityId>
        where TEntityId : notnull
    {
        Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default);
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
