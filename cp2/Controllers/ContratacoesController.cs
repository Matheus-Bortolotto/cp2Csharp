using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cp2.Data;
using cp2.Models;
using cp2.Services;

namespace cp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratacoesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ScoreService _score;

        public ContratacoesController(AppDbContext context, ScoreService score)
        {
            _context = context;
            _score = score;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Contratacao contratacao)
        {
            var cliente = await _context.Clientes.FindAsync(contratacao.IdCliente);
            if (cliente == null) return NotFound("Cliente não encontrado.");

            int scoreValor = 0;
            if (cliente is PessoaFisica pf)
            {
                var emprestimo = await _context.Produtos
                    .OfType<Emprestimo>()
                    .FirstOrDefaultAsync(e => e.IdProduto == contratacao.IdProduto);

                if (emprestimo != null)
                {
                    scoreValor = _score.CalcularScore(pf.DataNascimento, emprestimo.ValorSolicitado);
                    contratacao.Status = _score.Aprovado(scoreValor) ? "APROVADO" : "REJEITADO";
                }
            }
            else
            {
                contratacao.Status = "PENDENTE";
            }

            contratacao.DtSolicitacao = DateTime.Now;
            _context.Contratacoes.Add(contratacao);
            await _context.SaveChangesAsync();

            return Accepted(new { contratacao.IdContratacao, contratacao.Status, Score = scoreValor });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var contratacao = await _context.Contratacoes
                .Include(c => c.Cliente)
                .Include(c => c.Produto)
                .FirstOrDefaultAsync(c => c.IdContratacao == id);
            if (contratacao == null) return NotFound();
            return Ok(contratacao);
        }
    }
}