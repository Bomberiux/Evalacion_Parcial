using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Recursos_Humanos.Models
{
    public class UsuariosModel : IdentityUser
    {
        [Required]
        public string Cedula { get; set; }
    }
}
