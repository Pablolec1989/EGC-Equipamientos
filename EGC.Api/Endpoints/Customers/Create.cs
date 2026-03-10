using Application.Abstractions.Messaging;
using Application.Clients.Create;
using Application.Customers.Create;
using Application.Locations.Create;
using EGC.Api.Extensions;
using EGC.Api.Infrastructure;
using EGC.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EGC.Api.Endpoints.Customers
{
    public class Create : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/customers", async (
                CreateCustomerRequest request,
                ICommandHandler<CreateCustomerCommand, Guid> handler,
                CancellationToken cancellationToken) =>
            {
                var command = new CreateCustomerCommand
                {
                    Name = request.Name,
                    LastName = request.LastName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Location = request.Location,

                };
                Result<Guid> result = await handler.Handle(command, cancellationToken);

                return result.Match(
                    id => Results.Created($"/api/v1/customers/{id}", id),
                    CustomResults.Problem);

            }).WithTags(Tags.Customers);
        }
    }
}
