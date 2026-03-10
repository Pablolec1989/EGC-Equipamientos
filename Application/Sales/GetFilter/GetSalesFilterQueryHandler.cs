using Application.Abstractions.Messaging;
using Application.Sales.Shared;
using EGC.Domain.Entities.Sales;
using EGC.Domain.Repositories;
using EGC.Domain.Shared;
using System.Linq.Expressions;

namespace Application.Sales.GetFilter
{
    internal sealed class GetSalesFilterQueryHandler : IQueryHandler<GetSalesFilterRequest, List<SaleResponse>>
    {
        private readonly ISaleRepository _saleRepository;

        public GetSalesFilterQueryHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public Task<Result<List<SaleResponse>>> Handle(GetSalesFilterRequest query, CancellationToken cancellationToken)
        {
            IQueryable<Sale> salesQuery = _saleRepository.SaleQuery();

            // Filtro por término de búsqueda en el nombre del cliente
            if (!string.IsNullOrEmpty(query.searchTerm))
            {
                salesQuery = salesQuery.Where(s => s.Customer.Name.Contains(query.searchTerm));
            }

            // Filtro por rango de fechas
            if (query.startDate.HasValue)
            {
                salesQuery = salesQuery.Where(s => s.RegistrationDate >= query.startDate.Value);
            }
            if (query.endDate.HasValue)
            {
                salesQuery = salesQuery.Where(s => s.RegistrationDate <= query.endDate.Value);
            }

            //Ordenar por la columna especificada
            if (query.sortOrder?.ToLower() == "desc")
            {
                salesQuery = salesQuery.OrderByDescending(GetSortProperty(query));
            }
            else
            {
                salesQuery = salesQuery.OrderBy(GetSortProperty(query));
            }

            var sales = salesQuery
                .Select(p => new SaleResponse
                {
                    Id = p.Id,
                    CustomerName = p.Customer.Name,
                    TotalAmount = p.TotalAmount,
                    SaleItemResponse = p.SaleItems.Select(i => new SaleItemResponse
                    {
                        Product = i.Product.Name,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        SubTotal = i.SubTotal
                    }).ToList(),
                    RegistrationDate = p.RegistrationDate
                }).ToList();

            return Task.FromResult(Result.Success(sales));
        }

            private static Expression<Func<Sale, object>> GetSortProperty(GetSalesFilterRequest query)
            {
                return query.sortColumn?.ToLower() switch
                {
                    "clientname" => s => s.Customer.Name,
                    "registrationdate" => s => s.RegistrationDate,
                    _ => s => s.Customer.Name
                };
            }
        }
    }
