using Application.Abstractions.Messaging;

namespace Application.Products.Create
{
    public sealed record CreateProductCommand : ICommand<Guid>
    {
        public required string Name { get; init; }
        public string? Description { get; init; }
        public decimal? Price { get; init; }
        public int Stock { get; set; }
        public string? CodEGC { get; init; }
        public string? CodFab { get; init; }
        public string? SerialCode { get; init; }

    }
}
