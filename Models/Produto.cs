using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LojaAPI.Models;

[Table("Produtos")]

public class Produto : IValidatableObject
{
    [Key]
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100, ErrorMessage = "A descrição deve ter entre 5 e 25 caracteres", 
        MinimumLength = 5)]
    public string? Nome { get; set; }

    [Required]
    [Range(1, 10000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    [JsonIgnore]
    public int IdVendedor { get; set; }

    
    public Vendedor? Vendedor { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(this.Preco <= 0)
        {
            yield return new ValidationResult("O preço deve ser maior que zero",
                new[] {nameof(this.Preco)}
                );
        }
    }
}


 