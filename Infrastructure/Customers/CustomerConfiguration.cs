using EGC.Domain.Entities.Clients;
using EGC.Domain.Entities.Customers;
using EGC.Domain.Entities.Locations;
using EGC.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Customers
{
    internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.LastName).HasMaxLength(100);
            builder.Property(c => c.Email).HasMaxLength(100);
            builder.Property(c => c.PhoneNumber).HasMaxLength(20);

            //Relacion con Location
            builder.HasOne(c => c.Location)
                       .WithOne(l => l.Customer)
                       .HasForeignKey<Location>(l => l.CustomerId);



            builder.HasQueryFilter(c => !c.IsDeleted);

            builder.HasIndex(c => c.IsDeleted).HasFilter("IsDeleted = 0");

        }
    }
}
