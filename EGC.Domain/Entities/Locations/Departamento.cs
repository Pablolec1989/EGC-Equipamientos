using EGC.Domain.Abstractions;

namespace EGC.Domain.Entities.Locations
{
    public class Departamento : EntityBase<int>
    {
        public string Nombre { get; set; } = string.Empty;
        public int ProvinciaId { get; set; }
        public Provincia Provincia { get; set; } = null!;
        public ICollection<Localidad> Localidades { get; set; } = new List<Localidad>();

        private Departamento(int id) : base(id)
        {
        }
    }



}
