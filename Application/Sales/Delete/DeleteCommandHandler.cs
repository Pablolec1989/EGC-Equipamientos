using Application.Abstractions.Messaging;
using EGC.Domain.Entities.Sales;
using EGC.Domain.Repositories;
using EGC.Domain.Shared;

namespace Application.Sales.Delete
{
    public sealed class DeleteCommandHandler : ICommandHandler<DeleteSaleCommand>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCommandHandler(ISaleRepository saleRepository, IUnitOfWork unitOfWork)
        {
            _saleRepository = saleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(command.saleId, cancellationToken);
            if (sale is null)
            {
                return Result.Failure(SaleErrors.NotFound(command.saleId));
            }
            _saleRepository.Remove(sale);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
