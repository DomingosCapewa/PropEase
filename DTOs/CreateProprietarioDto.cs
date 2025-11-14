using System.ComponentModel.DataAnnotations;

namespace PropEase.API.DTOs
{
    public class CreateProprietarioDto
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Telefone { get; set; } = string.Empty;

        [Required]
        public string CPF { get; set; } = string.Empty;
    }
}
