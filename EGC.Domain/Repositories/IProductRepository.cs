using EGC.Domain.Entities.Products;

namespace EGC.Domain.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product, Guid>
    {
        Task<bool> GetByCodEGCAsync(string codEGC, CancellationToken cancellationToken);
        Task<int> GetProductCountAsync(CancellationToken cancellationToken);
    }
}
