using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cp2.Data;
using cp2.Models;

namespace cp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("pf")]
        public async Task<IActionResult> PostPF(PessoaFisica pf)
        {
            // Validar CPF duplicado (sem AnyAsync — bug Oracle)
            var existente = await _context.Clientes
                .OfType<PessoaFisica>()
                .FirstOrDefaultAsync(c => c.Cpf == pf.Cpf);
            if (existente != null) return BadRequest("CPF já cadastrado.");

            var agencia = await _context.Agencias.FindAsync(pf.IdAgencia);
            if (agencia == null) return NotFound("Agência não encontrada.");

            _context.Clientes.Add(pf);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = pf.IdCliente }, pf);
        }

        [HttpPost("pj")]
        public async Task<IActionResult> PostPJ(PessoaJuridica pj)
        {
            // Validar CNPJ duplicado (sem AnyAsync — bug Oracle)
            var existente = await _context.Clientes
                .OfType<PessoaJuridica>()
                .FirstOrDefaultAsync(c => c.Cnpj == pj.Cnpj);
            if (existente != null) return BadRequest("CNPJ já cadastrado.");

            var agencia = await _context.Agencias.FindAsync(pj.IdAgencia);
            if (agencia == null) return NotFound("Agência não encontrada.");

            _context.Clientes.Add(pj);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = pj.IdCliente }, pj);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Agencia)
                .FirstOrDefaultAsync(c => c.IdCliente == id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }
    }
}