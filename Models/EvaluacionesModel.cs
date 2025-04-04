using System.ComponentModel.DataAnnotations;

namespace Recursos_Humanos.Models
{
    public class EvaluacionesModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int EmpleadoId { get; set; }
        public int EvaluadorId { get; set; } // Id del evaluador (puede ser un jefe o un evaluador especializado)
        public decimal Calificacion { get; set; }
        public string Comentarios { get; set; }
        public string Periodo { get; set; } // Ejemplo: "Trimestral", "Anual", etc.

        // Propiedades de navegación
        public EmpleadosModel Empleado { get; set; }
        
    }
}
