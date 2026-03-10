using EGC.Domain.Abstractions;
using EGC.Domain.Entities.Customers;
using System;

namespace EGC.Domain.Entities.Locations
{
    public class Location : EntityBase<Guid>
    {
        public string? Address { get; set; }
        public Guid CustomerId { get; private set; }
        public int? ProvinciaId { get; set; }
        public int? DepartamentoId { get; set; }
        public int? LocalidadId { get; set; }

        public Customer Customer { get; set; } = null!;
        public Provincia? Provincia { get; set; }
        public Departamento? Departamento { get; set; }
        public Localidad? Localidad { get; set; }

        private Location(Guid id) : base(id)
        {
        }

        public Location(
            Guid id,
            string? address,
            Guid customerId,
            int? provinciaId,
            int? departamentoId,
            int? localidadId) : base(id)
        {
            Address = address;
            CustomerId = customerId;
            ProvinciaId = provinciaId;
            DepartamentoId = departamentoId;
            LocalidadId = localidadId;
        }

        //Create
        public static Location Create(
            string? address,
            Guid customerId,
            int? provinciaId,
            int? departamentoId,
            int? localidadId)
        {
            return new Location(
                Guid.NewGuid(), 
                address, 
                customerId, 
                provinciaId, 
                departamentoId, 
                localidadId);
        }

        //Update
        public void Update(
            string? address,
            Guid customerId,
            int? provinciaId,
            int? departamentoId,
            int? localidadId)
        {
            Address = address;
            CustomerId = customerId;
            ProvinciaId = provinciaId;
            DepartamentoId = departamentoId;
            LocalidadId = localidadId;
        }
    }

}
