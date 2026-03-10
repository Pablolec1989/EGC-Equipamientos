using EGC.Domain.Entities.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGC.Domain.Repositories
{
    public interface ILocalidadRepository : IRepositoryBase<Localidad, int>
    {
        Task<List<Localidad>> GetAllByDepartamentoId(int departamentoId, CancellationToken cancellationToken);
    }
}
