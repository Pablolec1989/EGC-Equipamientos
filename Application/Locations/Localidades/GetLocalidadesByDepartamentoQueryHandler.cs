using Application.Abstractions.Messaging;
using Application.Locations.Shared;
using EGC.Domain.Entities.Locations;
using EGC.Domain.Repositories;
using EGC.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Locations.Localidades
{
    public sealed class GetLocalidadesByDepartamentoQueryHandler
        : IQueryHandler<GetLocalidadesByDepartamentoQuery, List<LocalidadResponse>>
    {

        private readonly ILocalidadRepository _localidadRepository;

        public GetLocalidadesByDepartamentoQueryHandler(ILocalidadRepository localidadRepository)
        {
            _localidadRepository = localidadRepository;
        }

        public async Task<Result<List<LocalidadResponse>>> Handle(GetLocalidadesByDepartamentoQuery query, CancellationToken ct)
        {
            List<Localidad> localidades = await _localidadRepository.GetAllByDepartamentoId(query.DepartamentoId, ct);

            return localidades.Select(l => new LocalidadResponse
            {
                Id = l.Id,
                Nombre = l.Nombre,

            }).ToList();
        }
    }
}
