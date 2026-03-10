using Application.Abstractions.Messaging;
using Application.Sales.GetFilter;
using Application.Sales.Shared;
using EGC.Api.Infrastructure;
using EGC.Domain.Shared;

namespace EGC.Api.Endpoints.Sales
{
    public class GetSalesFilter : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/sales/filter", async (
                string? searchTerm,
                string? sortColumn,
                string? sortOrder,
                string? startDate,
                string? endDate,
                IQueryHandler<GetSalesFilterRequest, List<SaleResponse>> handler,
                CancellationToken cancellationToken) =>
            {
                DateOnly? start = null;
                DateOnly? end = null;

                if (!string.IsNullOrWhiteSpace(startDate) &&
                    !DateOnly.TryParseExact(startDate, "dd-MM-yyyy", out DateOnly startParsed))
                {
                    return CustomResults.Problem(Result.Failure(
                        Error.Validation("Sales.StartDate.Invalid", "startDate debe tener el formato dd-MM-yyyy")));
                }

                if (!string.IsNullOrWhiteSpace(startDate))
                {
                    DateOnly.TryParseExact(startDate, "dd-MM-yyyy", out DateOnly startParsed2);
                    start = startParsed2;
                }

                if (!string.IsNullOrWhiteSpace(endDate) &&
                    !DateOnly.TryParseExact(endDate, "dd-MM-yyyy", out DateOnly endParsed))
                {
                    return CustomResults.Problem(Result.Failure(
                        Error.Validation("Sales.EndDate.Invalid", "endDate debe tener el formato dd-MM-yyyy")));
                }

                if (!string.IsNullOrWhiteSpace(endDate))
                {
                    DateOnly.TryParseExact(endDate, "dd-MM-yyyy", out DateOnly endParsed2);
                    end = endParsed2;
                }


                var query = new GetSalesFilterRequest(
                    searchTerm,
                    sortColumn,
                    sortOrder,
                    start,
                    end);

                var result = await handler.Handle(query, cancellationToken);
                return Results.Ok(result);

            }).WithTags(Tags.Sales);
        }
    }
}
