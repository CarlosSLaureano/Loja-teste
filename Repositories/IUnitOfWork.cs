namespace LojaAPI.Repositories
{
    public interface IUnitOfWork
    {
        IProdutoRepository ProdutoRepository { get; }
        IVendedorRepository VendedorRepository { get; }
        Task CommitAsync();
    }
}

