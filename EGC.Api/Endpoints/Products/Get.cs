
using Application.Abstractions.Messaging;
using Application.Products.Get;
using Application.Products.Shared;

namespace EGC.Api.Endpoints.Products
{
    public class Get : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (
                IQueryHandler<GetProductsQuery, List<ProductResponse>> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetProductsQuery();
                var result = await handler.Handle(query, cancellationToken);
                return Results.Ok(result);

            }).WithTags(Tags.Products);
        }
    }
}
