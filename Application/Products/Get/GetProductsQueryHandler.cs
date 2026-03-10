using Application.Abstractions.Messaging;
using Application.Products.Shared;
using EGC.Domain.Repositories;
using EGC.Domain.Shared;

namespace Application.Products.Get
{
    internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, List<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<List<ProductResponse>>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);

            var response = products.Select(product => new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description ?? string.Empty,
                Price = product.Price ?? 0,
                Stock = product.Stock,
                CodEGC = product.CodEGC,
                CodFab = product.CodFab,
                SerialCode = product.SerialCode,

            }).ToList();

            return Result.Success(response);
        }
    }
}
