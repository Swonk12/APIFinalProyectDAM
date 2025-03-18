using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace APIFinalProyectDAM.MODELS
{
    public class ClContratos
    {
        [Key]
        public int IdContrato { get; set; } // Clave primaria

        [Required]
        public int IdUsuario { get; set; } // Clave foránea

        [Required]
        public string NombreArchivo { get; set; }

        [Required]
        public string RutaArchivo { get; set; }

        [BindProperty(SupportsGet = false)]
        public DateTime FechaSubida { get; set; } = DateTime.Now;
    }
}
