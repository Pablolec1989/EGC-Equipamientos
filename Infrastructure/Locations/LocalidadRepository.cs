using EGC.Domain.Entities.Locations;
using EGC.Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Locations
{
    public class LocalidadRepository : RepositoryBase<Localidad, int>, ILocalidadRepository
    {
        public LocalidadRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Localidad>> GetAllByDepartamentoId(int departamentoId, CancellationToken cancellationToken)
        {
            return await _context.Localidad
                .Where(localidad => localidad.DepartamentoId == departamentoId)
                .ToListAsync(cancellationToken);
        }
    }
}
