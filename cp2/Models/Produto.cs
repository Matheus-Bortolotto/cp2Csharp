using System.ComponentModel.DataAnnotations;

namespace cp2.Models
{
    public abstract class Produto
    {
        [Key]
        public int IdProduto { get; set; }
        public string? NmProduto { get; set; }
        public string? TipoProduto { get; set; }
    }
}