using Microsoft.EntityFrameworkCore;
using APIFinalProyectDAM.MODELS;  // Importa los modelos

namespace APIFinalProyectDAM.DATA
{
    public class ClDbContext : DbContext
    {
        // Constructor que recibe las opciones de configuración (como la cadena de conexión)
        public ClDbContext(DbContextOptions<ClDbContext> options) : base(options) { }

        // Definición de las tablas en el contexto
        public DbSet<ClUsuarios> Usuarios { get; set; }
        public DbSet<ClFichajes> Fichajes { get; set; }
        public DbSet<ClVacaciones> Vacaciones { get; set; }
        public DbSet<ClContratos> Contratos { get; set; }
        public DbSet<ClNominas> Nominas { get; set; }

        // Configuración detallada de las tablas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la tabla Usuarios
            modelBuilder.Entity<ClUsuarios>(entity =>
            {
                entity.ToTable("Usuarios");

                entity.HasKey(u => u.IdUsuario);

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
                      .HasDefaultValue("Usuario");

                entity.Property(u => u.FechaRegistro)
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(u => u.Estado)
                      .HasDefaultValue(true);
            });

            // Configuración de la tabla Fichajes
            modelBuilder.Entity<ClFichajes>(entity =>
            {
                entity.ToTable("Fichajes");

                entity.HasKey(f => f.IdFichaje);

                entity.Property(f => f.Fecha)
                      .IsRequired();

                entity.Property(f => f.HoraEntrada)
                      .IsRequired();

                entity.HasOne<ClUsuarios>()
                      .WithMany()
                      .HasForeignKey(f => f.IdUsuario)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de la tabla Vacaciones
            modelBuilder.Entity<ClVacaciones>(entity =>
            {
                entity.ToTable("Vacaciones");

                entity.HasKey(v => v.IdVacacion);

                entity.Property(v => v.FechaInicio)
                      .IsRequired();

                entity.Property(v => v.FechaFin)
                      .IsRequired();

                entity.Property(v => v.Estado)
                      .HasMaxLength(50)
                      .HasDefaultValue("Pendiente");

                entity.HasOne<ClUsuarios>()
                      .WithMany()
                      .HasForeignKey(v => v.IdUsuario)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de la tabla Contratos
            modelBuilder.Entity<ClContratos>(entity =>
            {
                entity.ToTable("Contratos");

                entity.HasKey(c => c.IdContrato);

                entity.Property(c => c.NombreArchivo)
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(c => c.RutaArchivo)
                      .HasMaxLength(500)
                      .IsRequired();

                entity.HasOne<ClUsuarios>()
                      .WithMany()
                      .HasForeignKey(c => c.IdUsuario)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de la tabla Nominas
            modelBuilder.Entity<ClNominas>(entity =>
            {
                entity.ToTable("Nominas");

                entity.HasKey(n => n.IdNomina);

                entity.Property(n => n.MesAnio)
                      .HasMaxLength(10)
                      .IsRequired();

                entity.Property(n => n.NombreArchivo)
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(n => n.RutaArchivo)
                      .HasMaxLength(500)
                      .IsRequired();

                entity.HasOne<ClUsuarios>()
                      .WithMany()
                      .HasForeignKey(n => n.IdUsuario)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
