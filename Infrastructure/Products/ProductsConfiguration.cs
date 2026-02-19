using EGC.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Products
{
    internal sealed class ProductsConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
                builder.Property(p => p.Precio).HasPrecision(18, 2);
                builder.HasIndex(p => p.CodEGC).IsUnique();
                builder.HasIndex(p => p.CodFab).IsUnique();
        }
    }
}
