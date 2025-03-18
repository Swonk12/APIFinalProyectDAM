using Microsoft.AspNetCore.Mvc;
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
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        [Required]
        [RegularExpression("Pendiente|Aprobado|Rechazado")]
        public string Estado { get; set; } = "Pendiente";

        [BindProperty(SupportsGet = false)]
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;
    }
}
