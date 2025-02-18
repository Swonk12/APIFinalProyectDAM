using Microsoft.EntityFrameworkCore;
using APIFinalProyectDAM.MODELS;  // Asegúrate de importar el modelo de tu clase Usuario

namespace APIFinalProyectDAM.DATA
{
    public class ClDbContext : DbContext
    {
        // Constructor que recibe las opciones de configuración (como la cadena de conexión)
        public ClDbContext(DbContextOptions<ClDbContext> options) : base(options) { }

        // Aquí es donde defines las tablas a las que accederás en la base de datos
        public DbSet<ClUsuarios> Usuarios { get; set; }  // Mapea tu clase Usuario a la tabla "Usuarios" en SQL Server

        // Si necesitas configurar más detalles de las tablas, lo puedes hacer en esta función
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aquí puedes agregar configuraciones si es necesario (como tamaños de columnas o restricciones)
            modelBuilder.Entity<ClUsuarios>()
                .Property(u => u.Nombre)
                .HasMaxLength(100)  // Limita el tamaño de la columna "Nombre"
                .IsRequired();      // Hace que "Nombre" no sea nulo

            modelBuilder.Entity<ClUsuarios>()
                .Property(u => u.Correo)
                .HasMaxLength(100)  // Limita el tamaño de la columna "Correo"
                .IsRequired();      // Hace que "Correo" no sea nulo

            modelBuilder.Entity<ClUsuarios>()
                .Property(u => u.Edad)
                .IsRequired();      // Hace que "Edad" no sea nulo
        }
    }
}
