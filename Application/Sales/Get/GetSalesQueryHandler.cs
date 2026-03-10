using Application.Abstractions.Messaging;
using Application.Sales.Shared;
using EGC.Domain.Repositories;
using EGC.Domain.Shared;

namespace Application.Sales.Get
{
    public sealed class GetSalesQueryHandler : IQueryHandler<GetSalesQuery, List<SaleResponse>>
    {
        private readonly ISaleRepository _saleRepository;

        public GetSalesQueryHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<Result<List<SaleResponse>>> Handle(GetSalesQuery query, CancellationToken ct)
        {
            var sales = await _saleRepository.GetAllSalesAsync(ct);

            var response = sales.Select(s => new SaleResponse
            {
                Id = s.Id,
                CustomerName = s.Customer.Name,
                TotalAmount = s.TotalAmount,
                SaleItemResponse = s.SaleItems.Select(si => new SaleItemResponse
                {
                    Product = si.Product.Name,
                    Quantity = si.Quantity,
                    UnitPrice = si.Product?.Price ?? 0,
                    SubTotal = si.Quantity * si.Product?.Price ?? 0
                }).ToList(),
                RegistrationDate = s.RegistrationDate,
            }).ToList();

            return Result.Success(response);
        }
    }
}
