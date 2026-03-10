using Application.Abstractions.DataBase;
using EGC.Domain.Abstractions;
using EGC.Domain.Entities.Customers;
using EGC.Domain.Entities.Locations;
using EGC.Domain.Entities.Products;
using EGC.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public sealed class AppDbContext(
     DbContextOptions<AppDbContext> options)
     : DbContext(options), IApplicationDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Localidad> Localidad { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var softDeleteEntries = ChangeTracker.Entries<ISoftDeletable>()
                .Where(e => e.State == EntityState.Deleted);

            foreach (var entityEntry in softDeleteEntries)
            {
                entityEntry.State = EntityState.Modified;
                entityEntry.Property(nameof(ISoftDeletable.IsDeleted)).CurrentValue = true;
            }

                // When should you publish domain events?
                //
                // 1. BEFORE calling SaveChangesAsync
                //     - domain events are part of the same transaction
                //     - immediate consistency
                // 2. AFTER calling SaveChangesAsync
                //     - domain events are a separate transaction
                //     - eventual consistency
                //     - handlers can fail

                int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return Database.BeginTransactionAsync(cancellationToken);
        }
    }
}
