using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIFinalProyectDAM.DATA;
using APIFinalProyectDAM.MODELS;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFinalProyectDAM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacacionesController : ControllerBase
    {
        private readonly ClDbContext _context;

        public VacacionesController(ClDbContext context)
        {
            _context = context;
        }

        // GET: api/vacaciones -> Obtiene todas las vacaciones de todos los usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClVacaciones>>> GetVacaciones()
        {
            var vacaciones = await _context.Vacaciones.ToListAsync();
            return Ok(vacaciones);
        }

        // GET: api/vacaciones/usuario/5 -> Obtiene las vacaciones de un usuario por ID
        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<ClVacaciones>>> GetVacacionesByUsuario(int idUsuario)
        {
            var vacaciones = await _context.Vacaciones
                                           .Where(v => v.IdUsuario == idUsuario)
                                           .ToListAsync();

            if (vacaciones == null || vacaciones.Count == 0)
            {
                return NotFound(new { mensaje = "No se encontraron vacaciones para el usuario" });
            }

            return Ok(vacaciones);
        }

        // POST: api/vacaciones -> Crea una nueva vacación
        [HttpPost]
        public async Task<ActionResult<ClVacaciones>> PostVacacion([FromBody] ClVacaciones vacacion)
        {
            if (vacacion == null)
            {
                return BadRequest(new { mensaje = "Datos inválidos" });
            }

            // Validar si ya hay una vacación para ese usuario y fecha
            var vacacionExistente = await _context.Vacaciones
                                                   .AnyAsync(v => v.IdUsuario == vacacion.IdUsuario &&
                                                                  v.Fecha.Date == vacacion.Fecha.Date);
            if (vacacionExistente)
            {
                return Conflict(new { mensaje = "Ya se ha registrado una vacación para este día" });
            }

            _context.Vacaciones.Add(vacacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVacacionesByUsuario), new { idUsuario = vacacion.IdUsuario }, vacacion);
        }


        // DELETE: api/vacaciones?usuario=5&fecha=2025-05-10
        [HttpDelete]
        public async Task<IActionResult> DeleteVacacion([FromQuery] int usuario, [FromQuery] string fecha)
        {
            var vacacion = await _context.Vacaciones
                                         .FirstOrDefaultAsync(v => v.IdUsuario == usuario && v.Fecha.Date == DateTime.Parse(fecha).Date);

            if (vacacion == null)
            {
                return NotFound(new { mensaje = "Vacación no encontrada" });
            }

            // Eliminar la vacación
            _context.Vacaciones.Remove(vacacion);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content, indicando que la eliminación fue exitosa
        }


        // PUT: api/vacaciones/5 -> Actualiza una vacación por ID (si es necesario)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacacion(int id, ClVacaciones vacacion)
        {
            if (id != vacacion.IdVacacion)
            {
                return BadRequest(new { mensaje = "El ID de la vacación no coincide" });
            }

            _context.Entry(vacacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Vacaciones.Any(e => e.IdVacacion == id))
                {
                    return NotFound(new { mensaje = "Vacación no encontrada" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}
