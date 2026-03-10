using EGC.Domain.Abstractions;

namespace EGC.Domain.Entities.Locations
{
    public class Localidad : EntityBase<int>
    {
        public string Nombre { get; set; } = string.Empty;
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; } = null!;

        private Localidad(int id) : base(id)
        {
        }
    }



}
