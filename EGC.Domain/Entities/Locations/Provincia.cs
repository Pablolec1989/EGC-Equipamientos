using EGC.Domain.Abstractions;

namespace EGC.Domain.Entities.Locations
{
    public class Provincia : EntityBase<int>
    {
        public string Nombre { get; set; } = string.Empty;
        public ICollection<Departamento> Departamentos { get; set; } = new List<Departamento>();

        private Provincia(int id) : base(id)
        {
        }
    }



}
