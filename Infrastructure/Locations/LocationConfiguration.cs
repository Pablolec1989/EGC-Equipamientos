using EGC.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGC.Domain.Entities.Locations
{
    internal sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.Id);

            // Clave foránea hacia Customer
            builder.Property(l => l.CustomerId)
                   .IsRequired();

            //Relacion con Provincia
            builder.HasOne(l => l.Provincia)
                   .WithMany()
                   .HasForeignKey(l => l.ProvinciaId)
                   .OnDelete(DeleteBehavior.SetNull);

            //Relacion con Departamento
            builder.HasOne(l => l.Departamento)
                   .WithMany()
                   .HasForeignKey(l => l.DepartamentoId)
                   .OnDelete(DeleteBehavior.SetNull);

            //Relacion con Localidad
            builder.HasOne(l => l.Localidad)
                   .WithMany()
                   .HasForeignKey(l => l.LocalidadId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
