using EGC.Domain.Entities.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGC.Domain.Repositories
{
    public interface IProvinciaRepository : IRepositoryBase<Provincia, int>
    {
        IQueryable<Provincia> GetAllQuery();
    }
}
