using Microsoft.AspNetCore.Mvc;
using cp2.Data;
using cp2.Models;

namespace cp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgenciasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AgenciasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Agencia agencia)
        {
            _context.Agencias.Add(agencia);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = agencia.IdAgencia }, agencia);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var agencia = await _context.Agencias.FindAsync(id);
            if (agencia == null) return NotFound();
            return Ok(agencia);
        }
    }
}