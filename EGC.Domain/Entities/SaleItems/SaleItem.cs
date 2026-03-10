using EGC.Domain.Abstractions;
using EGC.Domain.Entities.Products;
using EGC.Domain.Entities.Sales;
using System;

namespace EGC.Domain.Entities.SaleItems
{
    public class SaleItem : EntityBase<Guid>
    {
        public Guid SaleId { get; private set; }
        public Sale Sale { get; private set; }
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal SubTotal => Quantity * UnitPrice;


        private SaleItem(Guid Id) : base(Id)
        {
            Sale = null!;
            Product = null!;
        } // Para EF Core

        private SaleItem(Guid Id, Sale sale, Product product, int quantity) : base(Id)
        {
            Sale = sale ?? throw new ArgumentNullException(nameof(sale));
            SaleId = sale.Id;
            Product = product ?? throw new ArgumentNullException(nameof(product));
            ProductId = product.Id;
            Quantity = quantity;
            UnitPrice = product.Price ?? 0;
        }

        public static SaleItem Create(Sale sale, Product product, int quantity)
        {
            if (sale == null)
                throw new ArgumentNullException(nameof(sale));
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (quantity <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero", nameof(quantity));
            if (product.Price == null)
                throw new ArgumentException("El producto no tiene un precio definido", nameof(product));

            return new SaleItem(Guid.NewGuid(), sale, product, quantity);
        }

    }
}
