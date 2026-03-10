
using Application.Abstractions.Messaging;
using Application.Sales.Delete;
using EGC.Api.Extensions;
using EGC.Api.Infrastructure;
using EGC.Domain.Shared;

namespace EGC.Api.Endpoints.Sales
{
    public class DeleteSale : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("/sales/{saleId:guid}", async (
                Guid saleId, 
                ICommandHandler<DeleteSaleCommand> handler,
                CancellationToken cancellationToken) =>
            {
                var command = new DeleteSaleCommand(saleId);
                var result = await handler.Handle(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .WithTags(Tags.Sales);
        }
    }
}
