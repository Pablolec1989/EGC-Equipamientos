using EGC.Domain.Entities.Locations;
using EGC.Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Locations
{
    public class LocationRepository : RepositoryBase<Location, Guid>, ILocationRepository
    {
        public LocationRepository(AppDbContext context) : base(context)
        {
        }


        public IQueryable<Location> GetAllQuery()
        {
            return _context.Locations
                .Include(l => l.Provincia)
                .ThenInclude(p => p!.Departamentos)
                .ThenInclude(d => d.Localidades);
        }

        //Buscar por customerId
        public async Task<Location?> GetLocationByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await GetAllQuery()
                .FirstOrDefaultAsync(l => l.CustomerId == customerId, cancellationToken);
        }
    }
}
