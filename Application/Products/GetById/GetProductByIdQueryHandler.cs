using Application.Abstractions.Messaging;
using Application.Products.GetProductById;
using Application.Products.Shared;
using EGC.Domain.Entities.Products;
using EGC.Domain.Repositories;
using EGC.Domain.Shared;

namespace Application.Products.GetById
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            Product? product = await _productRepository.GetByIdAsync(query.productId, cancellationToken);

            if (product is null)
            {
                return Result.Failure<ProductResponse>(ProductErrors.NotFound(query.productId));
            }

            ProductResponse response = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description ?? string.Empty,
                Price = product.Price ?? 0,
                Stock = product.Stock,
                CodEGC = product.CodEGC,
                CodFab = product.CodFab,
                SerialCode = product.SerialCode,
            };

            return response;

        }
    }
}
