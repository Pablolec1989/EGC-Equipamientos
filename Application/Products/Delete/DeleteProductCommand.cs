using Application.Abstractions.Messaging;

namespace Application.Products.Delete
{
    public sealed record DeleteProductCommand(Guid productId) : ICommand
    {
        public Guid Id { get; init; }
    }
}
