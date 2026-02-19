using Application.Abstractions.Messaging;
using Application.Products.Shared;
using EGC.Domain.Shared;

namespace Application.Products.Get
{
    internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, List<ProductResponse>>
    {
        public Task<Result<List<ProductResponse>>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
