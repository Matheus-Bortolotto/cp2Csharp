namespace cp2.Models
{
    public class Emprestimo : Produto
    {
        public decimal ValorSolicitado { get; set; }
        public int PrazoMeses { get; set; }
        public decimal TaxaJuros { get; set; }
    }
}