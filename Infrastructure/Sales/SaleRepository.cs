using EGC.Domain.Entities.Sales;
using EGC.Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Sales
{
    public sealed class SaleRepository : RepositoryBase<Sale, Guid>, ISaleRepository
    {
        public SaleRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Sale> SaleQuery()
        {
            return _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.SaleItems)
                    .ThenInclude(si => si.Product);
        }

        public async Task<List<Sale>> GetAllSalesAsync(CancellationToken cancellationToken)
        {
            return await SaleQuery()
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Sale>> GetSalesCurrentMonthAsync(CancellationToken cancellationToken)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var firstDayOfCurrentMonth = new DateOnly(today.Year, today.Month, 1);
            var firstDayOfNextMonth = firstDayOfCurrentMonth.AddMonths(1);

            return await _context.Sales
                .Where(s => s.RegistrationDate >= firstDayOfCurrentMonth && s.RegistrationDate < firstDayOfNextMonth)
                .Include(s => s.Customer)
                .Include(s => s.SaleItems)
                    .ThenInclude(si => si.Product)
                .ToListAsync(cancellationToken);
        }

    }
}
