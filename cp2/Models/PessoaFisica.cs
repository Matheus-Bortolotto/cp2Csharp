namespace cp2.Models
{
    public class PessoaFisica : Cliente
    {
        public string? Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}