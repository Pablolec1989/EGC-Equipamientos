using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGC.Domain.Exceptions.Products
{
    public class ProductNameInvalidException : DomainException
    {
        public ProductNameInvalidException()
            : base("El nombre del producto es requerido o inválido.") { }
    }
}
