using Application.Abstractions.Messaging;

namespace Application.Products.Update
{
    public sealed record UpdateProductCommand(
        Guid ProductId,
        string Name,
        string Description,
        decimal Price,
        int Stock,
        string CodEGC,
        string CodFab,
        string SerialCode) : ICommand
    {
    }
}
