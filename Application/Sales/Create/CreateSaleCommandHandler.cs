using Application.Abstractions.DataBase;
using Application.Abstractions.Messaging;
using EGC.Domain.Entities.Clients;
using EGC.Domain.Entities.Customers;
using EGC.Domain.Entities.Sales;
using EGC.Domain.Repositories;
using EGC.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System;

namespace Application.Sales.Create
{
    internal sealed class CreateSaleCommandHandler : ICommandHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSaleCommandHandler(
            ISaleRepository saleRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IApplicationDbContext context,
            IUnitOfWork unitOfWork)
        {
            _saleRepository = saleRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _context.BeginTransactionAsync(cancellationToken);
            try
            {
                // Obtener el cliente
                Customer? customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
                if (customer == null)
                {
                    return Result.Failure<Guid>(CustomerErrors.NotFound(request.CustomerId));
                }

                // 2. Obtener productos asociados a la venta
                var productIds = request.SaleItemsCommand.Select(i => i.ProductId).ToList();

                // Recuperar productos desde la base de datos
                var products = await _context.Products
                    .Where(p => productIds.Contains(p.Id))
                    .ToListAsync(cancellationToken);

                // Validar existencia de todos los productos
                if (products.Count != productIds.Count)
                {
                    return Result.Failure<Guid>(Error.NotFound("Product.NotFound", "Uno o más productos no existen."));
                }


                // Reducir stock de cada producto
                foreach (var item in request.SaleItemsCommand)
                {
                    var product = products.First(p => p.Id == item.ProductId);
                    if (product.Stock < item.Quantity)
                    {
                        return Result.Failure<Guid>(Error.Conflict("Product.Stock", $"Stock insuficiente para el producto {product.Name}."));
                    }
                    product.ReduceStock(item.Quantity);
                    _productRepository.Update(product); 
                }

                var sale = Sale.Create(
                    Guid.NewGuid(),
                    customer,
                    request.SaleItemsCommand.Select(i => (products.First(p => p.Id == i.ProductId), i.Quantity))
                );

                // Guardar la venta
                _saleRepository.Add(sale);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return Result.Success(sale.Id);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception($"Error al crear la venta");
            }
        }
    }
}
