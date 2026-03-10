using Application.Abstractions.Messaging;
using Application.Locations.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Locations.Localidades
{
    public record GetLocalidadesByDepartamentoQuery(int DepartamentoId) : IQuery<List<LocalidadResponse>>
    {
    }
}
