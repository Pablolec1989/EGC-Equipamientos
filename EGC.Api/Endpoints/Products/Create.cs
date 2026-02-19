using Application.Abstractions.Messaging;
using Application.Products.Commands;
using EGC.Api.Endpoints;
using EGC.Api.Extensions;
using EGC.Api.Infrastructure;
using EGC.Domain.Shared;
using Web.Api.Endpoints;

namespace EGC.Api.Controllers.Products
{
    public class Create : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("products", async (
            ProductRequest request,
            ICommandHandler<CreateProductCommand, Guid> handler,
            CancellationToken cancellationToken) =>
            {
                var command = new CreateProductCommand
                {
                    Nombre = request.Nombre,
                    Precio = request.Precio,
                    CodEGC = request.CodEGC,
                    CodFab = request.CodFab
                };

                Result<Guid> result = await handler.Handle(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
        .WithTags(Tags.Products)
            .WithOpenApi();
            //.RequireAuthorization()
            ;
        }
    }
}
