using EGC.Domain.Entities.Locations;
using EGC.Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Locations
{
    public class ProvinciaRepository : RepositoryBase<Provincia, int>, IProvinciaRepository
    {
        public ProvinciaRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Provincia> GetAllQuery()
        {
            return _context.Provincia
                .OrderBy(p => p.Nombre)
                .Include(p => p.Departamentos)
                .ThenInclude(d => d.Localidades);
        }
    }
}
