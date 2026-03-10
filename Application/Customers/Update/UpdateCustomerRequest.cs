using Application.Locations.Create;

namespace Application.Clients.Update
{
    public sealed record UpdateCustomerRequest
    {

        public required string Name { get; init; }
        public string? LastName { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public required CreateLocationRequest Location { get; init; }
    }
}
