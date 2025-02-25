using Microsoft.EntityFrameworkCore;
using APIFinalProyectDAM.MODELS;  // Importa el modelo de Usuario

namespace APIFinalProyectDAM.DATA
{
    public class ClDbContext : DbContext
    {
        // Constructor que recibe las opciones de configuración (como la cadena de conexión)
        public ClDbContext(DbContextOptions<ClDbContext> options) : base(options) { }

        // Definición de la tabla "Usuarios"
        public DbSet<ClUsuarios> Usuarios { get; set; }

        // Configuración detallada de las tablas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la tabla Usuarios
            modelBuilder.Entity<ClUsuarios>(entity =>
            {
                entity.ToTable("Usuarios"); // Asegura que la tabla en la BD se llame "Usuarios"

                entity.HasKey(u => u.IdUsuario); // Define la clave primaria

                entity.Property(u => u.Nombre)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(u => u.Apellido)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(u => u.Email)
                      .HasMaxLength(150)
                      .IsRequired();

                entity.Property(u => u.Contrasena)
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(u => u.TipoUsuario)
                      .HasMaxLength(50)
                      .IsRequired()
                      .HasDefaultValue("Usuario"); // Valor por defecto "Usuario"

                entity.Property(u => u.FechaRegistro)
                      .HasDefaultValueSql("GETDATE()"); // Valor por defecto la fecha actual

                entity.Property(u => u.Estado)
                      .HasDefaultValue(true); // Usuario activo por defecto
            });
        }
    }
}
