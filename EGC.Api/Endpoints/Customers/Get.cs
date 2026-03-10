
using Application.Abstractions.Messaging;
using Application.Clients.Get;
using Application.Clients.Shared;

namespace EGC.Api.Endpoints.Customers
{
    public class Get : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/customers", async (
                IQueryHandler<GetCustomersQuery, List<CustomerResponse>> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetCustomersQuery();
                var result = await handler.Handle(query, cancellationToken);
                return Results.Ok(result);

            }).WithTags(Tags.Customers);
        }
    }
}
