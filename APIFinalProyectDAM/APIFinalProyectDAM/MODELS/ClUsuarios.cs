namespace APIFinalProyectDAM.MODELS
{
    public class ClUsuarios
    {
        public int IdUsuario { get; set; } // Clave primaria
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public string TipoUsuario { get; set; } // "Admin" o "Usuario"
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; } // Activo (true) o Inactivo (false)
    }
}
