namespace EGC.Api.Endpoints.Locations
{
    using Application.Abstractions.Messaging;
    using Application.Locations.Provincias.Get;
    using Application.Locations.Shared;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;

    public class Provincia : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/provincias", async (
                IQueryHandler<GetProvinciasQuery, List<ProvinciaResponse>> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetProvinciasQuery();
                var result = await handler.Handle(query, cancellationToken);
                return Results.Ok(result);
            }).WithTags(Tags.Locations);
        }

       
    }
}
