namespace Application.Products.Update
{
    public sealed record UpdateProductRequest
    {
        public required string Name { get; init; }
        public string Description { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int Stock { get; init; }
        public string CodEGC { get; init; } = string.Empty;
        public string CodFab { get; init; } = string.Empty;
        public string SerialCode { get; init; } = string.Empty;
    }
}
