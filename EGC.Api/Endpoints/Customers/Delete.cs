
using Application.Abstractions.Messaging;
using Application.Clients.Delete;
using EGC.Api.Extensions;
using EGC.Api.Infrastructure;
using EGC.Domain.Shared;
using System.Threading;

namespace EGC.Api.Endpoints.Customers
{
    public class Delete : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("/customers/{id:guid}", async (
                Guid id,
                ICommandHandler<DeleteCustomerCommand> handler,
                CancellationToken cancellationToken) =>
            {
                var command = new DeleteCustomerCommand(id);
                Result result = await handler.Handle(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);

            }).WithTags(Tags.Customers);
        }
    }
}
