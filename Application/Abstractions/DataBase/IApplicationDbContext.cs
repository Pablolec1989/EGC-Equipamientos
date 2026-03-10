using EGC.Domain.Entities.Customers;
using EGC.Domain.Entities.Locations;
using EGC.Domain.Entities.Products;
using EGC.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Abstractions.DataBase;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Sale> Sales { get; }
    DbSet<Location> Locations { get; }
    DbSet<Provincia> Provincia { get; }
    DbSet<Departamento> Departamento { get; }
    DbSet<Localidad> Localidad { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
