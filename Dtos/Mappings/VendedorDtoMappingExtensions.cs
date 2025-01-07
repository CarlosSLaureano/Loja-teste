using LojaAPI.Dtos;
using LojaAPI.Models;

namespace LojaAPI.DTOs.Mappings
{
    public static class VendedorDtoMappingExtensions
    {
        public static VendedorDto? ToVendedorDto(this Vendedor vendedor)
        {
            if (vendedor is null)
                return null;

            return new VendedorDto
            {
                IdVendedor = vendedor.IdVendedor,
                Nome = vendedor.Nome,
                Sobrenome = vendedor.Sobrenome,
            };
        }


        public static Vendedor? ToVendedor(this VendedorDto vendedorDto)
        {
            if(vendedorDto is null) return null;

            return new Vendedor
            {
                IdVendedor = vendedorDto.IdVendedor,
                Nome =vendedorDto.Nome,
                Sobrenome = vendedorDto.Sobrenome,
            };
        }

        public static IEnumerable<VendedorDto> ToVendedorDtoList
                (this IEnumerable<Vendedor> vendedores)
        {
            if (vendedores is null || !vendedores.Any())
            {
                return new List<VendedorDto>();
            } 

            return vendedores.Select(vendedor => new VendedorDto
            {
                IdVendedor = vendedor.IdVendedor,
                Nome = vendedor.Nome,
                Sobrenome= vendedor.Sobrenome,
            }).ToList();

        }
    }
}
