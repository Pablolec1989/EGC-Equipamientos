using Application.Abstractions.DataBase;
using EGC.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
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
    }
}
