using EGC.Domain.Entities.Customers;
using EGC.Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Clients
{
    public class CustomerRepository : RepositoryBase<Customer, Guid>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Customer>> GetAllByLocationIdAsync(CancellationToken cancellationToken = default)
        {
            return await GetAllQuery().ToListAsync(cancellationToken);

        }

        public IQueryable<Customer> GetAllQuery()
        {
            return _context.Customers.Include(c => c.Location)
                .ThenInclude(l => l!.Provincia)
                .ThenInclude(p => p!.Departamentos)
                .ThenInclude(d => d.Localidades);

        }

        public async Task<Customer?> GetWihLocationById(Guid locationId, CancellationToken cancellationToken = default)
        {
            return await GetAllQuery().FirstOrDefaultAsync(c => c.Id == locationId, cancellationToken);
        }
    }
}
