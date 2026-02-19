using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGC.Domain.Exceptions.Products
{
    public class ProductPriceInvalidException : DomainException
    {
        public ProductPriceInvalidException()
            : base("El precio del producto debe ser mayor a cero.") { }
    }
}
