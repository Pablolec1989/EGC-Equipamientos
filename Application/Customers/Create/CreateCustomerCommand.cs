using Application.Abstractions.Messaging;
using Application.Locations.Create;

namespace Application.Customers.Create
{
    public sealed record CreateCustomerCommand : ICommand<Guid>
    {
        public required string Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public CreateLocationRequest? Location { get; set; }
    }
}
