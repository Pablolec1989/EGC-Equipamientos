using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sales.Create
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("El ID del cliente es requerido");

            RuleFor(x => x.SaleItemsCommand)
                .NotEmpty().WithMessage("Debe incluir al menos un producto");

            RuleForEach(x => x.SaleItemsCommand).ChildRules(item =>
            {
                item.RuleFor(x => x.ProductId)
                    .NotEmpty().WithMessage("El ID del producto es requerido");

                item.RuleFor(x => x.Quantity)
                    .GreaterThan(0).WithMessage("La cantidad del producto debe ser mayor a 0");
            });
        }
    }
}
