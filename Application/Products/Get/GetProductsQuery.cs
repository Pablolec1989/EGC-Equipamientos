using Application.Abstractions.Messaging;
using Application.Products.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Get
{
    public sealed record GetProductsQuery : IQuery<List<ProductResponse>>
    {
    }
}
