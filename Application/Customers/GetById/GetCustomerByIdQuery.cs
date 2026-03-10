using Application.Abstractions.Messaging;
using Application.Clients.Shared;

namespace Application.Clients.GetById
{
    public sealed record GetCustomerByIdQuery(Guid CustomerId) : IQuery<CustomerResponse>
    {
    }
}
