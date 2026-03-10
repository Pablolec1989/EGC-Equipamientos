using Application.Abstractions.Messaging;
using Application.Locations.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Locations.Departamentos
{
    public sealed record GetDepartamentosByProvinciaQuery(int ProvinciaId) : IQuery<List<DepartamentoResponse>>
    {
    }
}
