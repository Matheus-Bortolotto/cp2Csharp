using System.ComponentModel.DataAnnotations;

namespace cp2.Models
{
    public class Agencia
    {
        [Key]
        public int IdAgencia { get; set; }
        public string? NmEndereco { get; set; }
        public string? Cep { get; set; }
    }
}