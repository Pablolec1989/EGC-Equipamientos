using Application.Abstractions.Messaging;
using EGC.Domain.Shared;

namespace Application.Products.Delete
{
    public sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        public Task<Result> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
