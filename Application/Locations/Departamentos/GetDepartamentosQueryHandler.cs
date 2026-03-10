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

namespace Application.Locations.Departamentos
{
    public class GetDepartamentosQueryHandler : IQueryHandler<GetDepartamentosByProvinciaQuery, List<DepartamentoResponse>>
    {
        private readonly IDepartamentoRepository _departamentoRepository;

        public GetDepartamentosQueryHandler(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
        }

        public async Task<Result<List<DepartamentoResponse>>> Handle(GetDepartamentosByProvinciaQuery query, CancellationToken cancellationToken)
        {
            List<Departamento> departamentos = await _departamentoRepository.GetAllByProvinciaId(query.ProvinciaId);

            return departamentos.Select(d => new DepartamentoResponse
            {
                Id = d.Id,
                Nombre = d.Nombre

            }).ToList();
        }
    }
}
