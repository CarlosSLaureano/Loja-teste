using LojaAPI.Validations;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LojaAPI.Models;

[Table("Vendedores")]

public class Vendedor : IValidatableObject
{

    public Vendedor()
    {
        Produtos = new Collection<Produto>();
    }

    [Key]
    public int IdVendedor { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100, ErrorMessage = "O nome deve ter entre 5 e 40 caracteres",
        MinimumLength = 5)]
    [PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }

    [Required]
    [StringLength (100)]
    public string? Sobrenome { get; set; }
    [JsonIgnore]
    public ICollection<Produto>? Produtos { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(this.Nome))
        {
            var primeiraLetra = this.Nome[0].ToString();
            if(primeiraLetra != primeiraLetra.ToUpper())
            {
                yield return new ValidationResult("A primeira letra do nome do vendedor deve ser maiúscula,",
                    new[] {nameof(this.Nome)}

                    
                    );
            }
        }
    }
}
