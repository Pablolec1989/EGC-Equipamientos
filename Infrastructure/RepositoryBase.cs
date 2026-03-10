using EGC.Domain.Abstractions;
using EGC.Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public abstract class RepositoryBase<TEntity, TEntityId> : IRepositoryBase<TEntity, TEntityId>
    //TEntity debe ser una clase que hereda de Entity<TEntityId>. Esto implica que TEntity tiene al menos una propiedad Id del tipo TEntityId.
        where TEntity : EntityBase<TEntityId>
    //TEntityId debe ser una clase (por ejemplo, string, Guid, o cualquier tipo de referencia), no un tipo primitivo como int o float.
        where TEntityId : notnull
    {
        protected readonly AppDbContext _context;

        protected RepositoryBase(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>()
                .ToListAsync(cancellationToken);
        }
        public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>()
                .FirstOrDefaultAsync(entity => entity.Id != null && entity.Id.Equals(id), cancellationToken);
        }
        public virtual void Add(TEntity entity)
        {
            _context.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            if (entity is ISoftDeletable softDeletable)
            {
                softDeletable.IsDeleted = true;
                _context.Update(entity);
            }
            else
            {
                _context.Remove(entity);
            }
        }

    }
}
