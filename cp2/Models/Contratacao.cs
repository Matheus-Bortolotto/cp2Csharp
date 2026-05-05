using System.ComponentModel.DataAnnotations;

namespace cp2.Models
{
    public class Contratacao
    {
        [Key]
        public int IdContratacao { get; set; }
        public int IdCliente { get; set; }
        public Cliente? Cliente { get; set; }
        public int IdProduto { get; set; }
        public Produto? Produto { get; set; }
        public string? Status { get; set; } = "PENDENTE";
        public DateTime DtSolicitacao { get; set; } = DateTime.Now;
    }
}