using Application.Abstractions.Messaging;
using EGC.Domain.Entities.Products;
using EGC.Domain.Repositories;
using EGC.Domain.Shared;

namespace Application.Products.Delete
{
    public sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Product? product = await _productRepository.GetByIdAsync(command.ProductId, cancellationToken);

                if (product == null)
                {
                    return Result.Failure(ProductErrors.NotFound(command.ProductId));
                }
                _productRepository.Remove(product);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {

                throw new Exception("Error", ex);
            }
        }
    }
}
