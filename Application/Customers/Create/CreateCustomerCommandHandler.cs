using Application.Abstractions.DataBase;
using Application.Abstractions.Messaging;
using EGC.Domain.Entities.Customers;
using EGC.Domain.Entities.Locations;
using EGC.Domain.Repositories;
using EGC.Domain.Shared;

namespace Application.Customers.Create
{
    internal sealed class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Guid>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationDbContext _context;

        public CreateCustomerCommandHandler(
            ICustomerRepository clientRepository,
            ILocationRepository locationRepository,
            IUnitOfWork unitOfWork,
            IApplicationDbContext context)
        {
            _customerRepository = clientRepository;
            _locationRepository = locationRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<Result<Guid>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            using var transaction = await _context.BeginTransactionAsync(cancellationToken);
            try
            {
                Customer customer = Customer.Create(
                command.Name,
                command.LastName,
                command.Email,
                command.PhoneNumber,
                null);

                _customerRepository.Add(customer);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                Location location = Location.Create(
                    command.Location?.Address,
                    customer.Id,
                    command.Location?.ProvinciaId,
                    command.Location?.DepartamentoId,
                    command.Location?.LocalidadId);

                _locationRepository.Add(location);

                customer.Update(
                    customer.Name,
                    customer.LastName,
                    customer.Email,
                    customer.PhoneNumber,
                    location);

                _customerRepository.Update(customer);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return Result.Success(customer.Id);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception($"Error al crear el cliente");
            }

        }
    }
}
