using AutoMapper;
using LojaAPI.Dtos;
using LojaAPI.Models;

namespace LojaAPI.DTOs.Mappings
{
    public class ProdutoDtoMappingProfile : Profile
    {
        public ProdutoDtoMappingProfile()
        {
            CreateMap<Produto, ProdutoDto>().ReverseMap();
            CreateMap<Vendedor, VendedorDto>().ReverseMap();
        }
    }
}
   
        
