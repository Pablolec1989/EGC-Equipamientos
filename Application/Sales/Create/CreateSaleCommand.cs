using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Sales.Create
{
    public record CreateSaleCommand(Guid CustomerId, List<SaleItemCommand> SaleItemsCommand) : ICommand<Guid>
    {
    }
    public record SaleItemCommand(Guid ProductId, int Quantity)
    {
    }
}
