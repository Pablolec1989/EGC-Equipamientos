namespace EGC.Domain.Entities.Products
{
    public sealed record ProductRequest
    {
        public string Nombre { get; init; }
        public decimal Precio { get; init; }
        public string CodEGC { get; init; }
        public string CodFab { get; init; }
    }
}
