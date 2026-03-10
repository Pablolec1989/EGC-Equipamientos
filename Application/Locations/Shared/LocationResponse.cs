using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Locations.Shared
{
    public record LocationResponse
    {
        public Guid Id { get; init; }
        public string? Address { get; init; }
        public ProvinciaResponse? Provincia { get; init; }
        public DepartamentoResponse? Departamento { get; init; }
        public LocalidadResponse? Localidad { get; init; }
    }
}
