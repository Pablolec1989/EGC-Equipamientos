
using Application.Abstractions.Messaging;
using Application.BusinessSumaries.Get;
using Application.BusinessSumaries.Shared;

namespace EGC.Api.Endpoints.BusinessSumaries
{
    public class Get : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/business-summary", async (
                IQueryHandler<GetBusinessSummaryQuery, BusinessSummaryResponse> handler, 
                CancellationToken ct) =>
            {
                var query = new GetBusinessSummaryQuery();
                var result = await handler.Handle(query, ct);
                return Results.Ok(result);

            }).WithTags(Tags.Summaries);
        }
    }
}
