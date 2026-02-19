using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGC.Api.Controllers.Products
{
    public sealed record ProductRequest
    {
        public string Nombre { get; init; }
        public decimal Precio { get; init; }
        public string CodEGC { get; init; }
        public string CodFab { get; init; }
    }
}
