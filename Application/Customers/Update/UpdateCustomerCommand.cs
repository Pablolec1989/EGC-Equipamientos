using Application.Abstractions.Messaging;
using Application.Locations.Create;

namespace Application.Clients.Update
{
    public sealed record UpdateCustomerCommand(Guid CustomerId) : ICommand
    {
        public required string Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public CreateLocationRequest? Location { get; set; }
    }
}
