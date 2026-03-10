using Application.Abstractions.Messaging;
using Application.Products.Delete;
using EGC.Api.Extensions;
using EGC.Api.Infrastructure;
using EGC.Domain.Shared;

namespace EGC.Api.Endpoints.Products
{
    public class Delete : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id:guid}", async (
            Guid id,
            ICommandHandler<DeleteProductCommand> handler,
            CancellationToken cancellationToken) =>
            {
                var command = new DeleteProductCommand(id);
                Result result = await handler.Handle(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .WithTags(Tags.Products);
        }
    }
}
