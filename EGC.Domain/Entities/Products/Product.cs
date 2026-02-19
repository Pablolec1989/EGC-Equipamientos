using Domain.Abstractions;
using EGC.Domain.Exceptions.Products;

namespace EGC.Domain.Entities.Products
{
    public class Product : EntityBase<Guid>
    {
        public string Nombre { get; private set; }
        public decimal Precio { get; private set; }
        public string CodEGC { get; private set; }
        public string CodFab { get; private set; }
        public DateTime FechaReg { get; private set; }
        public DateTime? FechaMod { get; private set; }

        private Product(Guid id, string nombre, decimal precio, string codEGC, string codFab, DateTime fechaReg, DateTime fechaMod) : base(id)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Precio = precio;
            CodEGC = codEGC;
            CodFab = codFab;
            FechaReg = fechaReg;
            FechaMod = fechaMod;
        }

        protected Product(Guid id) : base(id) { }

        public static Product Create(string nombre, decimal precio, string codEGC, string codFab, DateTime fechaReg, DateTime fechaMod)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ProductNameInvalidException();
            }
            if (precio <= 0)
            {
                throw new ProductPriceInvalidException();
            }
            return new Product(Guid.NewGuid(), nombre, precio, codEGC, codFab, fechaReg, fechaMod);

        }

        public void Update(string nombre, decimal precio, string codEGC, string codFab)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ProductNameInvalidException();
            }

            if (precio <= 0)
            {
                throw new ProductPriceInvalidException();
            }

            Nombre = nombre;
            Precio = precio;
            CodEGC = codEGC;
            CodFab = codFab;
            FechaMod = DateTime.UtcNow;
        }
    }
}
