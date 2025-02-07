﻿using LojaAPI.Context;

namespace LojaAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProdutoRepository? _produtoRepository;
        private IVendedorRepository? _vendedorRepository;

        public AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoRepository = _produtoRepository ?? new ProdutoRepository (_context);
            }
            
        }

        public IVendedorRepository VendedorRepository
        {
            get
            {
                return _vendedorRepository = _vendedorRepository ?? new VendedorRepository (_context);
            }
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
