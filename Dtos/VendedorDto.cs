using LojaAPI.Validations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LojaAPI.Dtos
{
    public class VendedorDto
    {
        [JsonIgnore]
        public int IdVendedor { get; set; }

        [Required]
        [StringLength(100)]
        [PrimeiraLetraMaiuscula]
        public string? Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string? Sobrenome { get; set; }
    }
}
