namespace Application.Products.Shared
{
    public sealed record ProductResponse
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
        public decimal Price { get; init; }
        public int Stock { get; init; }
        public string? CodEGC { get; init; }
        public string? CodFab { get; init; }
        public string? SerialCode { get; init; }

    }
}
