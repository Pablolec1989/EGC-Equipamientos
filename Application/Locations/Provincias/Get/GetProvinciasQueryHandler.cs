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

namespace Application.Locations.Provincias.Get
{
    internal sealed class GetProvinciasQueryHandler : IQueryHandler<GetProvinciasQuery, List<ProvinciaResponse>>
    {
        private readonly IProvinciaRepository _provinciaRepository;

        public GetProvinciasQueryHandler(IProvinciaRepository provinciaRepository)
        {
            _provinciaRepository = provinciaRepository;
        }

        public async Task<Result<List<ProvinciaResponse>>> Handle(GetProvinciasQuery query, CancellationToken cancellationToken)
        {
            List<Provincia> provincias = await _provinciaRepository.GetAllAsync();

            return provincias.Select(p => new ProvinciaResponse
            {
                Id = p.Id,
                Nombre = p.Nombre

            }).ToList();
        }
    }
}
