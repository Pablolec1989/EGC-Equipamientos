using Application.Locations.Create;

namespace Application.Clients.Create
{
    public sealed record CreateCustomerRequest
    {
        public required string Name { get; init; }
        public string? LastName { get; init; }
        public required string Email { get; init; }
        public required string PhoneNumber { get; init; }
        public required CreateLocationRequest Location { get; init; }
    }
}
