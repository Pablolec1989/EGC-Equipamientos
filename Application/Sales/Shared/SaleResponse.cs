namespace Application.Sales.Shared
{
    public sealed record SaleResponse
    {
        public Guid Id { get; init; }
        public required string CustomerName { get; init; }
        public decimal TotalAmount { get; init; }
        public required List<SaleItemResponse> SaleItemResponse { get; init; }
        public DateOnly RegistrationDate { get; init; }
    }

    public sealed record SaleItemResponse
    {
        public required string Product { get; init; }
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal SubTotal { get; init; }
    }

}
