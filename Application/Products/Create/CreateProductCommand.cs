using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Commands
{
    public sealed record CreateProductCommand : ICommand<Guid>
    {
        public Guid Id { get; init; }
        public string Nombre { get; init; }
        public decimal Precio { get; init; }
        public string CodEGC { get; init; }
        public string CodFab { get; init; }
    }
}
