
using Application.Abstractions.Messaging;
using Application.Sales.Create;
using EGC.Api.Extensions;
using EGC.Api.Infrastructure;
using EGC.Domain.Shared;

namespace EGC.Api.Endpoints.Sales
{
    public class CreateSale : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/sales", async (
                CreateSaleRequest request, 
                ICommandHandler<CreateSaleCommand, Guid> handler,
                CancellationToken cancellationToken) =>
            {
                var command = new CreateSaleCommand
                (
                    CustomerId: request.CustomerId,
                    SaleItemsCommand: request.SaleItemRequest.Select(i => 
                        new SaleItemCommand(i.ProductId, i.Quantity))
                    .ToList()
                );

                Result<Guid> result = await handler.Handle(command, cancellationToken);

                return result.Match(
                    id => Results.Created($"/api/v1/sales/{id}", id),
                    CustomResults.Problem);
            })
            .WithTags(Tags.Sales);
        }
    }
}
