using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIFinalProyectDAM.MODELS
{
    public class ClFichajes
    {
        [Key]
        public int IdFichaje { get; set; } // Clave primaria

        [Required]
        public int IdUsuario { get; set; } // Clave foránea

        [Required]
        public DateTime Fecha { get; set; } // Solo la fecha del fichaje

        [Required]
        public DateTime HoraEntrada { get; set; } // Ahora almacena fecha y hora de entrada

        public DateTime? HoraSalida { get; set; } // Puede ser NULL hasta que se fiche la salida

        public string? Comentarios { get; set; }
    }
}
