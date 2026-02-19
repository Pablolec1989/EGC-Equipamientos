using EGC.Domain.Entities.Products;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGC.Domain.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product, Guid>
    {
    }
}
