using Application.Abstractions.Messaging;
using Application.Products.Shared;
using EGC.Domain.Entities.Products;
using EGC.Domain.Repositories;
using EGC.Domain.Shared;

namespace Application.Products.Update
{
    internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            Product? product = await _productRepository.GetByIdAsync(command.ProductId, cancellationToken);

            if (product is null)
            {
                return Result.Failure(ProductErrors.NotFound(command.ProductId));
            }

            product.Update(command.Name, 
                command.Description, 
                command.Price, 
                command.Stock, 
                command.CodEGC,
                command.CodFab,
                command.SerialCode);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
