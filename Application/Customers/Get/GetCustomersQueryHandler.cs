using Application.Abstractions.Messaging;
using Application.Clients.Get;
using Application.Clients.Shared;
using Application.Locations.Shared;
using EGC.Domain.Repositories;
using EGC.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Get
{
    internal sealed class GetCustomersQueryHandler : IQueryHandler<GetCustomersQuery, List<CustomerResponse>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<List<CustomerResponse>>> Handle(GetCustomersQuery query, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllByLocationIdAsync(cancellationToken);

            return customers.Select(customer => new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                LastName = customer?.LastName,
                Email = customer?.Email,
                PhoneNumber = customer?.PhoneNumber,
                Location = customer?.Location is not null ? new LocationResponse
                {
                    Id = customer.Location.Id,
                    Provincia = customer.Location.Provincia is null ? null : new ProvinciaResponse
                    {
                        Id = customer.Location.Provincia.Id,
                        Nombre = customer.Location.Provincia.Nombre
                    },
                    Departamento = customer.Location.Departamento is null ? null : new DepartamentoResponse
                    {
                        Id = customer.Location.Departamento.Id,
                        Nombre = customer.Location.Departamento.Nombre
                    },
                    Localidad = customer.Location.Localidad is null ? null : new LocalidadResponse
                    {
                        Id = customer.Location.Localidad.Id,
                        Nombre = customer.Location.Localidad.Nombre
                    },
                    Address = customer.Location?.Address
                } : null
            }).ToList();

        }
    }
}
