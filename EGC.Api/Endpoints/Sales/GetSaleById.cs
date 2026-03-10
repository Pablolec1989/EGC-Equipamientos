
using Application.Abstractions.Messaging;
using Application.Clients.Shared;
using Application.Sales.GetById;
using Application.Sales.Shared;
using EGC.Api.Extensions;
using EGC.Api.Infrastructure;
using EGC.Domain.Shared;

namespace EGC.Api.Endpoints.Sales
{
    public class GetSaleById : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/sales/{saleId:guid}", async (
                Guid saleId,
                IQueryHandler<GetSaleByIdQuery, SaleResponse> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetSaleByIdQuery(saleId);
                Result<SaleResponse> result = await handler.Handle(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);

            })
            .WithTags(Tags.Sales);
        }
    }
}
