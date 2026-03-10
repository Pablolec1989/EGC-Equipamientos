using EGC.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Products
{
    internal sealed class ProductsConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(200);
            builder.Property(p => p.Price).HasPrecision(18, 2);
            builder.Property(p => p.Stock).IsRequired();
            builder.Property(p => p.CodEGC);
            builder.Property(p => p.CodFab);
            builder.Property(p => p.SerialCode);

            builder.HasIndex(p => p.CodEGC).IsUnique();
            builder.HasIndex(p => p.CodFab).IsUnique();
            builder.HasIndex(p => p.SerialCode).IsUnique();

            builder.HasQueryFilter(p => !p.IsDeleted);


            builder.HasIndex(p => p.IsDeleted).HasFilter("IsDeleted = 0");
        }
    }
}
