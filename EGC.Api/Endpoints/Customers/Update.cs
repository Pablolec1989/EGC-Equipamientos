
using Application.Abstractions.Messaging;
using Application.Clients.Update;
using EGC.Api.Extensions;
using EGC.Api.Infrastructure;
using EGC.Domain.Shared;

namespace EGC.Api.Endpoints.Customers
{
    public class Update : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("/customers/{customerId:guid}", async (
                Guid customerId, 
                UpdateCustomerRequest request, 
                ICommandHandler<UpdateCustomerCommand> handler,
                CancellationToken cancellationToken) =>
            {
                var command = new UpdateCustomerCommand(customerId)
                {
                    Name = request.Name,
                    LastName = request.LastName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Location = request.Location
                };

                Result result = await handler.Handle(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .WithTags(Tags.Customers);
        }
    }
}
