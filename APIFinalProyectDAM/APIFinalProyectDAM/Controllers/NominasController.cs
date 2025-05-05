using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIFinalProyectDAM.DATA;
using APIFinalProyectDAM.MODELS;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFinalProyectDAM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NominasController : ControllerBase
    {
        private readonly ClDbContext _context;

        public NominasController(ClDbContext context)
        {
            _context = context;
        }

        // GET: api/Nominas/usuario/5
        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<ClNominas>>> GetNominasPorUsuario(int idUsuario)
        {
            var nominas = await _context.Nominas
                                        .Where(n => n.IdUsuario == idUsuario)
                                        .ToListAsync();

            if (nominas == null || nominas.Count == 0)
            {
                return NotFound($"No se encontraron nóminas para el usuario con ID {idUsuario}");
            }

            return Ok(nominas);
        }

        // POST: api/Nominas
        [HttpPost]
        public async Task<ActionResult<ClNominas>> PostNomina(ClNominas nomina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            nomina.FechaSubida = DateTime.Now; // Asegura que la fecha se registre al momento de la subida

            _context.Nominas.Add(nomina);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNominasPorUsuario), new { idUsuario = nomina.IdUsuario }, nomina);
        }
    }
}
