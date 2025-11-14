using System.ComponentModel.DataAnnotations;

namespace PropEase.API.DTOs
{
    public class CreateImovelDto
    {
        [Required]
        [RegularExpression("^(?i)(casa|apartamento)$", ErrorMessage = "Tipo deve ser 'Casa' ou 'Apartamento'.")]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        [StringLength(250, MinimumLength = 3)]
        public string Endereco { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Numero { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "ProprietarioId deve ser maior que zero.")]
        public int ProprietarioId { get; set; }

        public bool Alugado { get; set; } = false;
    }
}