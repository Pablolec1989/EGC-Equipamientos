using Application.Abstractions.Messaging;
using Application.Sales.Shared;
using EGC.Domain.Entities.Sales;
using EGC.Domain.Repositories;
using EGC.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Sales.GetById
{
    public class GetSaleByIdQueryHandler : IQueryHandler<GetSaleByIdQuery, SaleResponse>
    {
        private readonly ISaleRepository _saleRepository;

        public GetSaleByIdQueryHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<Result<SaleResponse>> Handle(GetSaleByIdQuery query, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.SaleQuery()
                .FirstOrDefaultAsync(s => s.Id == query.saleId, cancellationToken);

            if (sale == null)
            {
                return Result.Failure<SaleResponse>(SaleErrors.NotFound(query.saleId));
            }

            var response = new SaleResponse
            {
                Id = sale.Id,
                CustomerName = sale.Customer.Name,
                TotalAmount = sale.TotalAmount,
                SaleItemResponse = sale.SaleItems.Select(si => new SaleItemResponse
                {
                    Product = si.Product.Name,
                    Quantity = si.Quantity,
                    UnitPrice = si.Product?.Price ?? 0,
                    SubTotal = si.Quantity * si.Product?.Price ?? 0
                }).ToList(),
                RegistrationDate = sale.RegistrationDate,
            };
            return Result.Success(response);
        }
    }
}
