

namespace Application.Locations.Create
{
    public sealed record CreateLocationRequest
    {
        public string? Address { get; init; }
        public int? ProvinciaId { get; init; }
        public int? DepartamentoId { get; init; }
        public int? LocalidadId { get; init; }

    }
}
