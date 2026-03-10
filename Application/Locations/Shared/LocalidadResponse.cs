namespace Application.Locations.Shared
{
    public record LocalidadResponse
    {
        public int Id { get; init; }
        public required string Nombre { get; init; }
    }
}
