namespace cp2.Models
{
    public class PessoaJuridica : Cliente
    {
        public string? Cnpj { get; set; }
        public string? RazaoSocial { get; set; }
    }
}