namespace Application.Locations.Shared
{
    public record DepartamentoResponse
    {
        public int Id { get; init; }
        public required string Nombre { get; init; }
    }
}
