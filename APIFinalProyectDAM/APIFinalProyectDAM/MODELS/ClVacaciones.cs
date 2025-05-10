using System.ComponentModel.DataAnnotations;

namespace APIFinalProyectDAM.MODELS
{
    public class ClVacaciones
    {
        [Key]
        public int IdVacacion { get; set; } // Clave primaria

        [Required]
        public int IdUsuario { get; set; } // Clave foránea

        [Required]
        public DateTime Fecha { get; set; } // Día individual de vacaciones
    }
}
