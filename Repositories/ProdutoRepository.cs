using LojaAPI.Context;
using LojaAPI.Models;
using LojaAPI.Pagination;
using X.PagedList;

namespace LojaAPI.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    { }

    public async Task<IPagedList<Produto>> GetProdutosAsync(ProdutosParameters produtosParameters)
    {
        var produtos = await GetAllAsync();

        var produtosOrdenados = produtos.OrderBy(p => p.ProdutoId).AsQueryable();

        var resultado = await produtosOrdenados.ToPagedListAsync(
                                                                 produtosParameters.PageNumber, 
                                                                 produtosParameters.PageSize);

        return resultado;
    }

    public async Task<IPagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroParameters)
    {
        var produtos = await GetAllAsync();


        if (produtosFiltroParameters.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroParameters.PrecoCriterio))
        {
            if (produtosFiltroParameters.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco > produtosFiltroParameters.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (produtosFiltroParameters.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco < produtosFiltroParameters.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (produtosFiltroParameters.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco == produtosFiltroParameters.Preco.Value).OrderBy(p => p.Preco);
            }
        }
        var produtosFiltrados = await produtos.ToPagedListAsync(produtosFiltroParameters.PageNumber,
                                                                produtosFiltroParameters.PageSize);
        return produtosFiltrados;
    }

    //public async Task<IEnumerable<Produto>> GetProdutosPorVendedorAsync(int? idVendedor)
    //{
    //    var produtos = await GetAllAsync();
    //    var produtosVendedor = produtos.Where(p => p.IdVendedor == idVendedor);
    //    return produtosVendedor;
    //}
}



