using Application.Abstractions.Messaging;
using Application.Sales.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sales.Get
{
    public sealed record GetSalesQuery : IQuery<List<SaleResponse>>
    {

    }
}
