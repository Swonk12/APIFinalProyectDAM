using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIFinalProyectDAM.DATA; // Asegúrate de que esté importado tu DbContext
using APIFinalProyectDAM.MODELS; // Asegúrate de importar tu clase Usuario

namespace APIFinalProyectDAM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ClDbContext _context;

        // Inyectamos el DbContext a través del constructor
        public UsuariosController(ClDbContext context)
        {
            _context = context;
        }

        // GET: api/usuarios -> Recoge todos los usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClUsuarios>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        // Ejemplo>> GET: api/usuarios/5 -> Coge usuario con un id que yo quiera
        [HttpGet("{id}")]
        public async Task<ActionResult<ClUsuarios>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult<ClUsuarios>> PostUsuario(ClUsuarios usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.ID }, usuario);
        }

        // PUT: api/usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, ClUsuarios usuario)
        {
            if (id != usuario.ID)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
