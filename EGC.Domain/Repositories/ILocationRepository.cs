using EGC.Domain.Entities.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGC.Domain.Repositories
{
    public interface ILocationRepository : IRepositoryBase<Location, Guid>
    {
        public IQueryable<Location> GetAllQuery();
        Task<Location?> GetLocationByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
    }
}
