using Application.Abstractions.Messaging;
using Application.Sales.Shared;

namespace Application.Sales.GetFilter
{
    public sealed record GetSalesFilterRequest
        
        (string? searchTerm,
         string? sortColumn,
         string? sortOrder,
         DateOnly? startDate,
         DateOnly? endDate) : IQuery<List<SaleResponse>>
    {
    }
}
