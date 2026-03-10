using Application.Abstractions.DataBase;
using Application.Abstractions.Messaging;
using Application.Clients.Update;
using EGC.Domain.Entities.Clients;
using EGC.Domain.Entities.Customers;
using EGC.Domain.Entities.Locations;
using EGC.Domain.Repositories;
using EGC.Domain.Shared;
using System;

namespace Application.Customers.Update
{
    internal sealed class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationDbContext _context;

        public UpdateCustomerCommandHandler(
            ICustomerRepository customerRepository, 
            ILocationRepository locationRepository,
            IUnitOfWork unitOfWork,
            IApplicationDbContext context)
        {
            _customerRepository = customerRepository;
            _locationRepository = locationRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<Result> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            using var transaction = await _context.BeginTransactionAsync(cancellationToken);
            try
            {
                Customer? customer = await _customerRepository.GetByIdAsync(command.CustomerId);

                if (customer is null)
                {
                    return Result.Failure(CustomerErrors.NotFound(command.CustomerId));
                }

                Location? location = await _locationRepository.GetLocationByCustomerIdAsync(command.CustomerId);

                if (location is null)
                {
                    location = Location.Create(
                        command.Location?.Address,
                        command.CustomerId,
                        command.Location?.ProvinciaId,
                        command.Location?.DepartamentoId,
                        command.Location?.LocalidadId
                    );
                    _locationRepository.Add(location);
                }
                else
                {
                    location.Update(
                        command.Location?.Address,
                        location.CustomerId,
                        command.Location?.ProvinciaId,
                        command.Location?.DepartamentoId,
                        command.Location?.LocalidadId);
                    _locationRepository.Update(location);
                }

                customer.Update(
                    command.Name,
                    command?.LastName,
                    command?.Email,
                    command?.PhoneNumber,
                    location);

                _customerRepository.Update(customer);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception($"Error al actualizar el cliente");
            }

        }
    }
}
