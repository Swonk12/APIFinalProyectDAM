using APIFinalProyectDAM.DATA;
using APIFinalProyectDAM.MODELS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIFinalProyectDAM.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FichajesController : ControllerBase
    {
        private readonly ClDbContext _context;

        public FichajesController(ClDbContext context)
        {
            _context = context;
        }

        // Fichar Entrada
        [HttpPost("entrada")]
        public async Task<IActionResult> FicharEntrada([FromBody] int idUsuario)
        {
            if (!_context.Usuarios.Any(u => u.IdUsuario == idUsuario))
                return BadRequest("Usuario no encontrado.");

            var ahora = DateTime.Now;

            var nuevoFichaje = new ClFichajes
            {
                IdUsuario = idUsuario,
                Fecha = ahora.Date,
                HoraEntrada = ahora
            };

            _context.Fichajes.Add(nuevoFichaje);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Entrada fichada correctamente.", nuevoFichaje });
        }

        // Fichar Salida
        [HttpPost("salida")]
        public async Task<IActionResult> FicharSalida([FromBody] int idUsuario)
        {
            var fichajeAbierto = await _context.Fichajes
                .Where(f => f.IdUsuario == idUsuario && f.HoraSalida == null)
                .OrderByDescending(f => f.IdFichaje)
                .FirstOrDefaultAsync();

            if (fichajeAbierto == null)
                return BadRequest("No hay fichaje abierto para cerrar.");

            fichajeAbierto.HoraSalida = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Salida fichada correctamente.", fichajeAbierto });
        }

        // Obtener fichajes de un usuario en un día específico
        [HttpGet("{idUsuario}/{fecha}")]
        public async Task<IActionResult> GetFichajesUsuario(int idUsuario, DateTime fecha)
        {
            var fichajes = await _context.Fichajes
                .Where(f => f.IdUsuario == idUsuario && f.Fecha == fecha.Date)
                .OrderBy(f => f.HoraEntrada)
                .ToListAsync();

            if (!fichajes.Any())
                return NotFound("No hay fichajes registrados para esta fecha.");

            return Ok(fichajes);
        }
    }

}
