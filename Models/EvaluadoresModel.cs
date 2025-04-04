using System.ComponentModel.DataAnnotations;

namespace Recursos_Humanos.Models
{
    public class EvaluadoresModel
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cargo { get; set; }
        public string Email { get; set; }

        // Propiedad de navegación: Evaluaciones realizadas por el evaluador
        public ICollection<EvaluacionesModel> Evaluaciones { get; set; }
    }
}
