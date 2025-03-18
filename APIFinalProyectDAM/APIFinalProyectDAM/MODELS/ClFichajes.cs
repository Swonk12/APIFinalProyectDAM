using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace APIFinalProyectDAM.MODELS
{
    public class ClFichajes
    {
        [Key]
        public int IdFichaje { get; set; } // Clave primaria

        [Required]
        public int IdUsuario { get; set; } // Clave foránea

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public TimeSpan HoraEntrada { get; set; }

        public TimeSpan? HoraSalida { get; set; }

        public string? Comentarios { get; set; }
    }
}
