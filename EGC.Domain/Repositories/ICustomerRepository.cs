using EGC.Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGC.Domain.Repositories
{
    public interface ICustomerRepository : IRepositoryBase<Customer, Guid>
    {
        IQueryable<Customer> GetAllQuery();
        Task<Customer?> GetWihLocationById(Guid locationId, CancellationToken cancellationToken = default);
        Task<List<Customer>> GetAllByLocationIdAsync(CancellationToken cancellationToken = default);
    }
}
