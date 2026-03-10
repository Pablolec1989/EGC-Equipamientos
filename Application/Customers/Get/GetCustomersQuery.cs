using Application.Abstractions.Messaging;
using Application.Clients.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Clients.Get
{
    public sealed record GetCustomersQuery : IQuery<List<CustomerResponse>>
    {
    }
}
