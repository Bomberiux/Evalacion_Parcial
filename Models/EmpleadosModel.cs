using System.ComponentModel.DataAnnotations;

namespace Recursos_Humanos.Models
{
    public class EmpleadosModel
    {
        [Key]
        public int Id { get; set; }
    public string Nombre { get; set; }
    public string Puesto { get; set; }
    public int DepartamentoId { get; set; }
    public decimal Salario { get; set; }

    // Propiedad de navegación: Relación con Departamento
    public DepartamentosModel Departamento { get; set; }

    // Propiedad de navegación: Relación con Evaluaciones
    public ICollection<EvaluacionesModel> Evaluaciones { get; set; }
    }
}
