using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Update
{
    public sealed record UpdateProductCommand (
        Guid ProductId, 
        string Nombre, 
        decimal Precio, 
        string CodEGC,
        string CodFab
    ) : ICommand
    {
    }
}
