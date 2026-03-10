using Application.Abstractions.Messaging;
using Application.Products.Create;
using EGC.Api.Extensions;
using EGC.Api.Infrastructure;
using EGC.Domain.Shared;

namespace EGC.Api.Endpoints.Products
{
    public class Create : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (
            CreateProductRequest request,
            ICommandHandler<CreateProductCommand, Guid> handler,
            CancellationToken cancellationToken) =>
            {
                var command = new CreateProductCommand
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Stock = request.Stock,
                    CodEGC = request.CodEGC,
                    CodFab = request.CodFab,
                    SerialCode = request.SerialCode
                };

                Result<Guid> result = await handler.Handle(command, cancellationToken);

                return result.Match(
                    id => Results.Created($"/api/v1/products/{id}", id),
                    CustomResults.Problem);
            })
            .WithTags(Tags.Products);
        }
    }
}
