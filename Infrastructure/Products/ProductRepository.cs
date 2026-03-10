using EGC.Domain.Entities.Products;
using EGC.Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Products
{
    public class ProductRepository : RepositoryBase<Product, Guid>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> GetByCodEGCAsync(string codEGC, CancellationToken cancellationToken)
        {
            return await _context.Products
                .AsNoTracking()
                .AnyAsync(p => p.CodEGC == codEGC, cancellationToken);


        }

        public Task<int> GetProductCountAsync(CancellationToken cancellationToken)
        {
            return _context.Products.CountAsync(cancellationToken);
        }
    }
}
