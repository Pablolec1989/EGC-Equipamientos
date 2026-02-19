using EGC.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.DataBase;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
