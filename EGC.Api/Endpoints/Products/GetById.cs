using Application.Abstractions.Messaging;
using Application.Products.GetProductById;
using Application.Products.Shared;
using EGC.Api.Extensions;
using EGC.Api.Infrastructure;
using EGC.Domain.Shared;

namespace EGC.Api.Endpoints.Products
{
    internal sealed class GetById : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id:guid}", async (
            Guid id,
            IQueryHandler<GetProductByIdQuery, ProductResponse> handler,
            CancellationToken cancellationToken) =>
            {
                var query = new GetProductByIdQuery(id);

                Result<ProductResponse> result = await handler.Handle(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);

            }).WithTags(Tags.Products);
        }
    }
}
