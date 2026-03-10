using Application.Abstractions.Messaging;
using Application.Products.Update;
using EGC.Api.Extensions;
using EGC.Api.Infrastructure;
using EGC.Domain.Shared;

namespace EGC.Api.Endpoints.Products
{
    public class Update : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("/products/{id:guid}", async (
                Guid id,
                UpdateProductRequest request,
                ICommandHandler<UpdateProductCommand> handler,
                CancellationToken cancellationToken) =>
            {
                var command = new UpdateProductCommand(
                    id, 
                    request.Name,
                    request.Description,
                    request.Price,
                    request.Stock,
                    request.CodEGC,
                    request.CodFab,
                    request.SerialCode);

                Result result = await handler.Handle(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .WithTags(Tags.Products);
            //.RequireAuthorization();
        }
    }
}
