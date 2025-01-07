using LojaAPI.Dtos;
using LojaAPI.Models;
using LojaAPI.Pagination;
using X.PagedList;

namespace LojaAPI.Repositories
{
    public interface IVendedorRepository : IRepository<Vendedor>
    {
        Task<IPagedList<Vendedor>> GetVendedoresAsync(VendedoresParameters vendedoresParameters);
        Task<IPagedList<Vendedor>> GetVendedoresFiltroNomeAsync(VendedoresFiltroNome vendedoresParameters);
        //Task<IPagedList<Vendedor>> BuscarVendedorPorId(int idVendedor);
        //Task<IPagedList<Vendedor>> BuscarVendedorPorIdProduto(int idProduto);
        //Task<IPagedList<Vendedor>> CriarVendedor(VendedorDto vendedorDto);
        //Task<IPagedList<Vendedor>> EditarVendedor(VendedorDto vendedorDto);
        //Task<IPagedList<Vendedor>> DeletarVendedor(int idVendedor);

    }

}

