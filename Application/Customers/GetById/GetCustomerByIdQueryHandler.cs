using Application.Abstractions.Messaging;
using Application.Clients.Shared;
using Application.Locations.Shared;
using EGC.Domain.Entities.Clients;
using EGC.Domain.Entities.Customers;
using EGC.Domain.Repositories;
using EGC.Domain.Shared;
using System;

namespace Application.Clients.GetById
{
    public class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, CustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<CustomerResponse>> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
        {
            Customer? customer = await _customerRepository.GetWihLocationById(query.CustomerId, cancellationToken);

            if (customer == null)
            {
                return Result.Failure<CustomerResponse>(CustomerErrors.NotFound(query.CustomerId));
            }

            CustomerResponse response = new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                LastName = customer?.LastName,
                Email = customer?.Email,
                PhoneNumber = customer?.PhoneNumber,
                Location = customer?.Location is null ? null : new LocationResponse
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

                }
            };
            return response;

        }
    }
}
