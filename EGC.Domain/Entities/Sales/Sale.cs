using EGC.Domain.Abstractions;
using EGC.Domain.Entities.Customers;
using EGC.Domain.Entities.Products;
using EGC.Domain.Entities.SaleItems;

namespace EGC.Domain.Entities.Sales
{
    public class Sale : EntityBase<Guid>
    {
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public decimal TotalAmount { get; private set; }
        public DateOnly RegistrationDate { get; private set; }
        public List<SaleItem> SaleItems { get; private set; }

        protected Sale(Guid Id) : base(Id)
        {
            Customer = null!;
            SaleItems = new();
        } // Para EF Core

        public Sale(Guid id, Customer customer) : base(id)
        {
            Customer = customer;
            CustomerId = customer.Id;
            TotalAmount = 0;
            RegistrationDate = DateOnly.FromDateTime(DateTime.Now);
            SaleItems = new();
        }

        public static Sale Create(Guid id, Customer customer, IEnumerable<(Product product, int quantity)> items)
        {
            var sale = new Sale(id, customer);
            sale.SaleItems = items
                .Select(i => SaleItem.Create(sale, i.product, i.quantity))
                .ToList();

            sale.TotalAmount = sale.SaleItems.Sum(i => i.SubTotal);
            return sale;
        }

    }
}
