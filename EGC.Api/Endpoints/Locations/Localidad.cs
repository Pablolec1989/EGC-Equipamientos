
using Application.Abstractions.Messaging;
using Application.Locations.Localidades;
using Application.Locations.Shared;
using EGC.Api.Extensions;
using EGC.Api.Infrastructure;
using EGC.Domain.Shared;

namespace EGC.Api.Endpoints.Locations
{
    public class Localidad : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/departamentos/{departamentoId:int}/localidades", 
                async (
                    int departamentoId,
                    IQueryHandler<GetLocalidadesByDepartamentoQuery, List<LocalidadResponse>> handler,
                    CancellationToken ct) =>
            {
                var query = new GetLocalidadesByDepartamentoQuery(departamentoId);
                Result<List<LocalidadResponse>> result = await handler.Handle(query, ct);
                return result.Match(Results.Ok, CustomResults.Problem);

            }).WithTags(Tags.Locations);
        }
    }
}
