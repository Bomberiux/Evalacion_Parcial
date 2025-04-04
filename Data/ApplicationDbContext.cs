using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recursos_Humanos.Models;

namespace Recursos_Humanos.Data
{
    public class ApplicationDbContext : IdentityDbContext<UsuariosModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<DepartamentosModel> Departamentos { get; set; }
        public DbSet<EmpleadosModel> Empleados { get; set; }
        public DbSet<EvaluacionesModel> Evaluaciones { get; set; }
        public DbSet<EvaluadoresModel> Evaluadores { get; set; }
        public DbSet<SalariosModel> Salarios { get; set; }

        // Configuración de las columnas decimal
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de las propiedades de tipo decimal
            modelBuilder.Entity<EmpleadosModel>()
                .Property(e => e.Salario)
                .HasColumnType("decimal(18,2)");  // 18 dígitos en total, 2 después del punto decimal

            modelBuilder.Entity<EvaluacionesModel>()
                .Property(e => e.Calificacion)
                .HasColumnType("decimal(18,2)");  // 18 dígitos en total, 2 después del punto decimal

            modelBuilder.Entity<SalariosModel>()
                .Property(s => s.Monto)
                .HasColumnType("decimal(18,2)");  // 18 dígitos en total, 2 después del punto decimal
        }
    }
}
