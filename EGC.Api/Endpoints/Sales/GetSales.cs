
using Application.Abstractions.Messaging;
using Application.Sales.Get;
using Application.Sales.Shared;

namespace EGC.Api.Endpoints.Sales
{
    public class GetSales : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/sales", async (
                IQueryHandler<GetSalesQuery, List<SaleResponse>> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetSalesQuery();
                var result = await handler.Handle(query, cancellationToken);
                return Results.Ok(result);
            })
            .WithTags(Tags.Sales);
        }
    }
}
