using EGC.Domain.Abstractions;
using EGC.Domain.Shared;

namespace EGC.Domain.Entities.Products
{
    public class Product : EntityBase<Guid>, ISoftDeletable
    {
        public string Name { get; private set; }
        public decimal? Price { get; private set; }
        public string? Description { get; private set; }
        private int _stock;
        public int Stock
        {
            get => _stock;
            private set
            {
                if (value < 0)
                    throw new ArgumentException("El stock no puede ser negativo", nameof(Stock));
                _stock = value;
            }
        }
        public string? CodEGC { get; private set; }
        public string? CodFab { get; private set; }
        public string? SerialCode { get; private set; }
        public DateOnly RegistrationDate { get; private set; }
        public DateOnly LastModifiedDate { get; private set; }
        public bool IsDeleted { get; set; }


        //Ctor
        protected Product(Guid Id) : base(Id)
        {
            Name = string.Empty;
        }

        private Product(Guid Id, 
            string name, 
            string? description, 
            decimal? price, 
            int stock, 
            string? codEGC, 
            string? codFab,
            string? serialCode) : base(Id)
        {
            Name = name;
            Description = description;
            Price = price ?? 0;
            Stock = stock;
            CodEGC = codEGC;
            CodFab = codFab;
            SerialCode = serialCode;
            RegistrationDate = DateOnly.FromDateTime(DateTime.Now);
            LastModifiedDate = RegistrationDate;
        }

        //Create
        public static Product Create(string name, string? description, decimal? price, int stock, string? codEGC, string? codFab, string? serialCode)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del producto no puede estar vacío", nameof(name));
            if (price < 0)
                throw new ArgumentException("El precio no puede ser negativo", nameof(price));
            if (stock < 0)
                throw new ArgumentException("El stock no puede ser negativo", nameof(stock));
            
            return new Product(Guid.NewGuid(), name, description, price, stock, codEGC, codFab, serialCode);
        }

        //Update
        public void Update(string name, string? description, decimal? price, int stock, string? codEGC, string? codFab, string? serialCode)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del producto no puede estar vacío", nameof(name));
            if (price < 0)
                throw new ArgumentException("El precio no puede ser negativo", nameof(price));
            if (stock < 0)
                throw new ArgumentException("El stock no puede ser negativo", nameof(stock));
            
            Name = name;
            Description = description ?? Description;
            Price = price ?? Price;
            Stock = stock;
            CodEGC = codEGC ?? CodEGC;
            CodFab = codFab ?? CodFab;
            SerialCode = serialCode ?? SerialCode;
            LastModifiedDate = DateOnly.FromDateTime(DateTime.Now);
        }

        //Reduce stock
        public void ReduceStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("La cantidad a reducir debe ser mayor a cero", nameof(quantity));
            if (quantity > Stock)
                throw new InvalidOperationException("No hay suficiente stock para reducir la cantidad solicitada");
            Stock -= quantity;
            LastModifiedDate = DateOnly.FromDateTime(DateTime.Now);
        }

        //Increase stock
        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("La cantidad a aumentar debe ser mayor a cero", nameof(quantity));
            Stock += quantity;
            LastModifiedDate = DateOnly.FromDateTime(DateTime.Now);
        }

    }
}
