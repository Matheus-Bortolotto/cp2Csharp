using System.ComponentModel.DataAnnotations;

namespace cp2.Models
{
    public abstract class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        public string? NmCliente { get; set; }
        public string? Email { get; set; }
        public string? TipoCliente { get; set; }

        public int IdAgencia { get; set; }
        public Agencia? Agencia { get; set; }
    }
}