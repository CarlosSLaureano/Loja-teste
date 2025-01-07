using LojaAPI.Context;
using LojaAPI.Dtos;
using LojaAPI.Models;
using LojaAPI.Pagination;
using X.PagedList;

namespace LojaAPI.Repositories;

public class VendedorRepository : Repository<Vendedor>, IVendedorRepository
{
    public VendedorRepository(AppDbContext context) : base(context) 
    { }

    public async Task<IPagedList<Vendedor>> GetVendedoresAsync(VendedoresParameters vendedoresParameters)
    {
        var vendedores = await GetAllAsync();

        var vendedoresOrdenados = vendedores.OrderBy(v => v.IdVendedor).AsQueryable();

        var resultado = await vendedoresOrdenados.ToPagedListAsync(vendedoresParameters.PageNumber,
                                                                   vendedoresParameters.PageSize);

        return resultado;
    }

    public async Task<IPagedList<Vendedor>> GetVendedoresFiltroNomeAsync(
        VendedoresFiltroNome vendedoresParameters)
    {
        var vendedores = await GetAllAsync();

        if (!string.IsNullOrEmpty(vendedoresParameters.Nome))
        {
            vendedores = vendedores.Where(v => v.Nome.Contains(vendedoresParameters.Nome));
        }

        var vendedoresFiltroNome = await vendedores.ToPagedListAsync(vendedoresParameters.PageNumber,
                                                                vendedoresParameters.PageSize);
        return vendedoresFiltroNome;
    }

}



    
  


   
