using Application.Locations.Shared;

namespace Application.Clients.Shared
{
    public sealed record CustomerResponse
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public string? LastName { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public LocationResponse? Location { get; init; }
    }
}
