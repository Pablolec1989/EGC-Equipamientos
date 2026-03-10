
using Application.Abstractions.Messaging;
using Application.Locations.Departamentos;
using Application.Locations.Shared;
using EGC.Api.Extensions;
using EGC.Api.Infrastructure;
using EGC.Domain.Shared;

namespace EGC.Api.Endpoints.Locations
{
    public class Departamento : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/provincias/{provinciaId:int}/departamentos",
                async (
                    int provinciaId,
                    IQueryHandler<GetDepartamentosByProvinciaQuery, List<DepartamentoResponse>> handler,
                    CancellationToken ct) =>
                {
                    var query =  new GetDepartamentosByProvinciaQuery(provinciaId);
                    Result<List<DepartamentoResponse>> result = await handler.Handle(query, ct);
                    return result.Match(Results.Ok, CustomResults.Problem);
                })
                .WithTags(Tags.Locations);
        }
    }
}
