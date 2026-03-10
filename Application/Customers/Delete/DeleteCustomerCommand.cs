
using Application.Abstractions.Messaging;

namespace Application.Clients.Delete
{
    public sealed record DeleteCustomerCommand(Guid CustomerId) : ICommand
    {
    }
}
