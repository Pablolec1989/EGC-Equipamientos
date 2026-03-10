namespace Application.Locations.Shared
{
    public record ProvinciaResponse
    {
        public int Id { get; init; }
        public required string Nombre { get; init; }
    }
}
