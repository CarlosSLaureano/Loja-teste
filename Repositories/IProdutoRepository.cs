using LojaAPI.Models;
using LojaAPI.Pagination;
using X.PagedList;

namespace LojaAPI.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<IPagedList<Produto>> GetProdutosAsync(ProdutosParameters produtosParameters);
    Task<IPagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroParameters);
    //Task<IEnumerable<Produto>> GetProdutosPorVendedorAsync(int? idVendedor);
}

