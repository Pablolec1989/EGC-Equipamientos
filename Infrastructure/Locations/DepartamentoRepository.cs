using EGC.Domain.Entities.Locations;
using EGC.Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Locations
{
    public class DepartamentoRepository : RepositoryBase<Departamento, int>, IDepartamentoRepository
    {
        public DepartamentoRepository(AppDbContext context) : base(context)
        {
        }

        public Task<List<Departamento>> GetAllByProvinciaId(int provinciaId)
        {
            return _context.Departamento
                .Where(d => d.ProvinciaId == provinciaId)
                .ToListAsync();
        }
    }
}
