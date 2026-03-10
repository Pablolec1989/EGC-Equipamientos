using Application.Abstractions.Messaging;
using EGC.Domain.Entities.Products;
using EGC.Domain.Repositories;
using EGC.Domain.Shared;

namespace Application.Products.Create
{
    internal sealed class ProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //Se valida si el usuario esta logueado y autorizado
            //if (userContext.UserId != command.UserId)
            //{
            //    return Result.Failure<Guid>(UserErrors.Unauthorized());
            //}

            //User? user = await context.Users.AsNoTracking()
            //    .SingleOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

            //if (user is null)
            //{
            //    return Result.Failure<Guid>(UserErrors.NotFound(command.UserId));
            //}

            //Evaluar si llega CodEGC, si llega validar que no exista otro producto con el mismo CodEGC


            if (!string.IsNullOrEmpty(command.CodEGC))
            {
                var existingProduct = await _productRepository.GetByCodEGCAsync(command.CodEGC, cancellationToken);
                if (existingProduct)
                {
                    return Result.Failure<Guid>(ProductErrors.CodEGCAlreadyExists(command.CodEGC));
                }
            }

            Product product = Product.Create
                (command.Name,
                command.Description,
                command.Price,
                command.Stock,
                command.CodEGC,
                command.CodFab,
                command.SerialCode);

            _productRepository.Add(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
