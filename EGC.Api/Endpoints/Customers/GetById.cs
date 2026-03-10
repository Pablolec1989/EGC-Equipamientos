

using Application.Abstractions.Messaging;
using Application.Clients.GetById;
using Application.Clients.Shared;
using EGC.Api.Extensions;
using EGC.Api.Infrastructure;
using EGC.Domain.Shared;

namespace EGC.Api.Endpoints.Customers
{
    internal sealed class GetById : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/customers/{id:guid}", async (
                Guid id,
                IQueryHandler<GetCustomerByIdQuery, CustomerResponse> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetCustomerByIdQuery(id);
                Result<CustomerResponse> result = await handler.Handle(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);

            }).WithTags(Tags.Customers);
        }
    }
}
