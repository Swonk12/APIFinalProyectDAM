using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace APIFinalProyectDAM.MODELS
{
    public class ClUsuarios
    {
        [Key]
        public int IdUsuario { get; set; } // Clave primaria

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Contrasena { get; set; }

        [Required]
        public string TipoUsuario { get; set; } // "Admin" o "Usuario"

        [BindProperty(SupportsGet = false)]
        public DateTime FechaRegistro { get; set; } // Lo maneja la BD

        public bool Estado { get; set; } // Activo (true) o Inactivo (false)
    }
}
