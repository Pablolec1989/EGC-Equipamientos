using EGC.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGC.Domain.Entities.Sales
{
    public class SaleErrors
    {
        public static Error AlreadyCompleted(Guid saleId) => Error.Problem(
            "Sale.AlreadyCompleted",
            $"The sale item with Id = '{saleId}' is already completed.");

        public static Error NotFound(Guid saleId) => Error.NotFound(
            "Sale.NotFound",
            $"The sale item with the Id = '{saleId}' was not found");
    }
}
