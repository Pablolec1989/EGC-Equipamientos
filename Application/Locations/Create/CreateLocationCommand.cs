using Application.Abstractions.Messaging;
using Application.Locations.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Locations.Create
{
    public class CreateLocationCommand : ICommand<Guid>
    {
        public Guid CustomerId { get; set; }
        public string? Address { get; set; }
        public int ProvinciaId { get; set; }
        public int DepartamentoId { get; set; }
        public int LocalidadId { get; set; }
    }
}
