using EGC.Domain.Abstractions;
using EGC.Domain.Entities.Locations;

namespace EGC.Domain.Entities.Customers
{
    public class Customer : EntityBase<Guid>, ISoftDeletable
    {
        public string Name { get; private set; }
        public string? LastName { get; private set; }
        public Guid? LocationId { get; private set; }
        public Location? Location { get; private set; }
        public string? Email { get; private set; }
        public string? PhoneNumber { get; private set; }

        public DateOnly RegistrationDate { get; private set; }
        public DateOnly? LastModifiedDate { get; private set; }
        public bool IsDeleted { get; set; }

        protected Customer(Guid id) : base(id)
        {
            Name = string.Empty;
        }

        private Customer(
            Guid Id, 
            string name, 
            string? lastName,
            string? email, 
            string? phoneNumber,
            Location? location) : base(Id)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Location = location;
            RegistrationDate = DateOnly.FromDateTime(DateTime.Now);
            LastModifiedDate = RegistrationDate;
        }

        //Create
        public static Customer Create(
            string name, 
            string? lastName,
            string? email, 
            string? phoneNumber,
            Location? location)
        {
            return new Customer(Guid.NewGuid(), name, lastName, email, phoneNumber, location);
        }

        //Update
        public void Update(
            string name, 
            string? lastName, 
            string? email, 
            string? phoneNumber,
            Location? location)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            LocationId = location?.Id;
            Location = location;
            LastModifiedDate = DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
