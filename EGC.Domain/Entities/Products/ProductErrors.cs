using EGC.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGC.Domain.Entities.Products
{
    public class ProductErrors
    {
        public static Error AlreadyCompleted(Guid productId) => Error.Problem(
            "Products.AlreadyCompleted",
            $"The product item with Id = '{productId}' is already completed.");

        public static Error CodEGCAlreadyExists(string codEGC)
        {
            return Error.Conflict(
                "Products.CodEGCAlreadyExists",
                $"El producto con el código EGC = '{codEGC}' ya existe.");

        }

        public static Error NotFound(Guid productId) => Error.NotFound(
            "Product.NotFound",
            $"The product item with the Id = '{productId}' was not found");
    }
}
