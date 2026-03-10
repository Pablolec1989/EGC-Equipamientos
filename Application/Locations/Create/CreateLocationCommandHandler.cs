using Application.Abstractions.Messaging;
using EGC.Domain.Entities.Clients;
using EGC.Domain.Entities.Customers;
using EGC.Domain.Entities.Locations;
using EGC.Domain.Repositories;
using EGC.Domain.Shared;
using System;

namespace Application.Locations.Create
{
    public class CreateLocationCommandHandler : ICommandHandler<CreateLocationCommand, Guid>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IProvinciaRepository _provinciaRepository;
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly ILocalidadRepository _localidadRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLocationCommandHandler(
            ICustomerRepository customerRepository,
            ILocationRepository locationRepository, 
            IProvinciaRepository provinciaRepository, 
            IDepartamentoRepository departamentoRepository, 
            ILocalidadRepository localidadRepository, 
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _locationRepository = locationRepository;
            _provinciaRepository = provinciaRepository;
            _departamentoRepository = departamentoRepository;
            _localidadRepository = localidadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateLocationCommand command, CancellationToken cancellationToken)
        {
            //crear transaction
            try
            {
                //Validar existencia de Customer
                Customer? customer = await _customerRepository.GetByIdAsync(command.CustomerId, cancellationToken);
                if (customer is null)
                {
                    return Result.Failure<Guid>(CustomerErrors.NotFound(command.CustomerId));
                }

                Location location = Location.Create(
                    command.Address,
                    command.CustomerId,
                    command.ProvinciaId,
                    command.DepartamentoId,
                    command.LocalidadId
                    );
                await _unitOfWork.SaveChangesAsync();
                return Result.Success(location.Id);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
