using Microsoft.AspNetCore.Mvc;
using cp2.Data;
using cp2.Models;

namespace cp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Emprestimo emprestimo)
        {
            _context.Produtos.Add(emprestimo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = emprestimo.IdProduto }, emprestimo);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }
    }
}