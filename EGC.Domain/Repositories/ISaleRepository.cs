using EGC.Domain.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGC.Domain.Repositories
{
    public interface ISaleRepository : IRepositoryBase<Sale, Guid>
    {
        IQueryable<Sale> SaleQuery();
        Task<List<Sale>> GetAllSalesAsync(CancellationToken cancellationToken);
        Task<List<Sale>> GetSalesCurrentMonthAsync(CancellationToken cancellationToken);
    }
}
