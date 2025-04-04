using System.ComponentModel.DataAnnotations;

namespace Recursos_Humanos.Models
{
    public class DepartamentosModel
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Propiedad de navegación: Relación con Empleados
        public ICollection<EmpleadosModel> Empleados { get; set; }
    }
}
