using Domain.Abstractions;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public abstract class RepositoryBase<TEntity, TEntityId> : IRepositoryBase<TEntity, TEntityId>
    //TEntity debe ser una clase que hereda de Entity<TEntityId>. Esto implica que TEntity tiene al menos una propiedad Id del tipo TEntityId.
        where TEntity : EntityBase<TEntityId>
    //TEntityId debe ser una clase (por ejemplo, string, Guid, o cualquier tipo de referencia), no un tipo primitivo como int o float.
        where TEntityId : notnull
    {
        protected readonly AppDbContext _appDbContext;
        protected RepositoryBase(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }
        public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Set<TEntity>()
                .ToListAsync(cancellationToken);
        }
        public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Set<TEntity>()
                .FirstOrDefaultAsync(entity => entity.Id!.Equals(id), cancellationToken);
        }
        public virtual void Add(TEntity entity)
        {
            _appDbContext.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _appDbContext.Update(entity);
        }
        public virtual void Remove(TEntity entity)
        {
            _appDbContext.Remove(entity);
        }

    }
}
