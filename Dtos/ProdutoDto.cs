using LojaAPI.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LojaAPI.Dtos
{
    
     public class ProdutoDto
     {
         [JsonIgnore]
         public int ProdutoId { get; set; }

         [Required]
         [StringLength(50)]
         public string? Nome { get; set; }
         [Required]
         
         public decimal Preco { get; set; }
         
         public int IdVendedor { get; set; }

     }
   
}
