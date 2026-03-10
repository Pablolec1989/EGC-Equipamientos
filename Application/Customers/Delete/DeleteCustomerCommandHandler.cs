using Application.Abstractions.Messaging;
using EGC.Domain.Entities.Clients;
using EGC.Domain.Entities.Customers;
using EGC.Domain.Repositories;
using EGC.Domain.Shared;

namespace Application.Clients.Delete
{
    internal sealed class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerCommandHandler(ICustomerRepository clientRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            Customer? client = await _clientRepository.GetByIdAsync(command.CustomerId, cancellationToken);

            if (client == null)
            {
                return Result.Failure(CustomerErrors.NotFound(command.CustomerId));
            }
            _clientRepository.Remove(client);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
