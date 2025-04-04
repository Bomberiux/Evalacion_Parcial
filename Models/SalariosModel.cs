using System.ComponentModel.DataAnnotations;

namespace Recursos_Humanos.Models
{
    public class SalariosModel
    {
        [Key]
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaVigencia { get; set; }

        // Propiedad de navegación
        public EmpleadosModel Empleado { get; set; }
    }
}
