using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIFinalProyectDAM.DATA;
using APIFinalProyectDAM.MODELS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIFinalProyectDAM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ClDbContext _context;

        public UsuariosController(ClDbContext context)
        {
            _context = context;
        }

        // GET: api/usuarios -> Obtiene todos los usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClUsuarios>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        // GET: api/usuarios/5 -> Obtiene un usuario por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ClUsuarios>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado" });
            }

            return Ok(usuario);
        }

        // POST: api/usuarios -> Crea un nuevo usuario
        // CONTROLADOR QUE AÑADE UN USUARIO DESDE EL HOME DEL ADMINISTRADOR
        [HttpPost]
        public async Task<ActionResult<ClUsuarios>> PostUsuario([FromBody] ClUsuarios usuario)
        {
            if (usuario == null)
            {
                return BadRequest(new { mensaje = "Datos inválidos" });
            }

            // Verificar si el email ya existe
            var emailExistente = await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email);
            if (emailExistente)
            {
                return BadRequest(new { mensaje = "El email ya está en uso" });
            }

            // Asignar valores predeterminados
            usuario.Estado = false; // Usuario inactivo por defecto
            usuario.FechaRegistro = DateTime.UtcNow; // Fecha actual en UTC

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
        }


        // PUT: api/usuarios/5 -> Actualiza un usuario
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, ClUsuarios usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest(new { mensaje = "El ID del usuario no coincide" });
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuarios.Any(e => e.IdUsuario == id))
                {
                    return NotFound(new { mensaje = "Usuario no encontrado" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/usuarios/5 -> Elimina un usuario por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado" });
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent(); // Retorna 204 No Content, indicando que la eliminación fue exitosa
        }

        // Peticion de Login 
        public class UserData
        {
            public string Email { get; set; }
            public string Contrasena { get; set; }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserData data)
        {
            try
            {

                var userdata = new UserData()
                {
                    Email = data.Email,
                    Contrasena = data.Contrasena,
                };

                if (string.IsNullOrEmpty(userdata.Email) || string.IsNullOrEmpty(userdata.Contrasena))
                {
                    return BadRequest(new { mensaje = "Email y contraseña son obligatorios" });
                }

                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == userdata.Email);

                if (usuario == null || usuario.Contrasena != userdata.Contrasena) // ⚠️ Usa hashing en producción
                {
                    return Unauthorized(new { mensaje = "Credenciales incorrectas" });
                }

                return Ok(new { id = usuario.IdUsuario, nombre = usuario.Nombre, email = usuario.Email, tipoUsuario = usuario.TipoUsuario });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno", detalle = ex.Message });
            }
        }


    }
}
