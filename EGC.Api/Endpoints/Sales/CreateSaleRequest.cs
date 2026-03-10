namespace EGC.Api.Endpoints.Sales
{
    public class CreateSaleRequest
    {
        public Guid CustomerId { get; init; }
        public required List<SaleItemRequest> SaleItemRequest { get; init; }
    }

    public class SaleItemRequest
    {
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
    }

}
