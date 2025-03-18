using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace APIFinalProyectDAM.MODELS
{
    public class ClNominas
    {
        [Key]
        public int IdNomina { get; set; } // Clave primaria

        [Required]
        public int IdUsuario { get; set; } // Clave foránea

        [Required]
        public string MesAnio { get; set; } // Ejemplo: "01-2024"

        [Required]
        public string NombreArchivo { get; set; }

        [Required]
        public string RutaArchivo { get; set; }

        [BindProperty(SupportsGet = false)]
        public DateTime FechaSubida { get; set; } = DateTime.Now;
    }
}
