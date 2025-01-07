using AutoMapper;
using LojaAPI.Dtos;
using LojaAPI.Filters;
using LojaAPI.Models;
using LojaAPI.Pagination;
using LojaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;

namespace LojaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<ProdutosController>? _logger;
        private readonly IMapper _mapper = mapper;
        
       

        [HttpGet("ListarTodosOsProdutos")]

        public async Task<ActionResult<IEnumerable<ProdutoDto>>> Get()
        {

            var produtos = await _unitOfWork.ProdutoRepository.GetAllAsync();
            if (produtos is null)
            {
                return NotFound("Não existem produtos...");
            }

            var produtosDto = _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
            return Ok(produtosDto);

        }



        [HttpGet("pagination")]

        public async Task<ActionResult<IEnumerable<ProdutoDto>>> Get([FromQuery]
                                        ProdutosParameters produtosParameters)
        {
            var produtos = await _unitOfWork.ProdutoRepository.GetProdutosAsync(produtosParameters);

            return ObterProdutos(produtos);
        }



        [HttpGet("filter/preco/pagination")]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetProdutosFilterPreco([FromQuery] ProdutosFiltroPreco
                                                                                        produtosFilterParameters)
        {
            var produtos = await _unitOfWork.ProdutoRepository.GetProdutosFiltroPrecoAsync(produtosFilterParameters);
            return ObterProdutos(produtos);
        }
        private ActionResult<IEnumerable<ProdutoDto>> ObterProdutos(IPagedList<Produto> produtos)
        {

            var metadata = new
            {
                produtos.Count,
                produtos.PageSize,
                produtos.PageCount,
                produtos.TotalItemCount,
                produtos.HasNextPage,
                produtos.HasPreviousPage
            };

            Response.Headers.Append("x-Pagination", JsonConvert.SerializeObject(metadata));

            var produtosDto = _mapper.Map<IEnumerable<ProdutoDto>>(produtos);

            return Ok(produtosDto);

        }



        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]

        public async Task<ActionResult<ProdutoDto>> Get(int id)
        {
            var produto = await _unitOfWork.ProdutoRepository.GetAsync(c => c.ProdutoId == id);
            if (produto is null)
            {
                return NotFound($"Produtoi com id = {id} não encontrado, tente outro id...");
            }

            var produtoDto = _mapper.Map<ProdutoDto>(produto);
            return Ok(produtoDto);

        }



        [HttpPost("CriarProdutoComIdVendedor")]

        public async Task<ActionResult<ProdutoDto>> Post(ProdutoDto produtoDto)
        {
            if (produtoDto is null)
            {
                //_logger.LogWarning("Dados inválidos...");
                return BadRequest("Dados inválidos...");
            }

            var produto = _mapper.Map<Produto>(produtoDto);

            var novoProduto = _unitOfWork.ProdutoRepository.Create(produto);
            await _unitOfWork.CommitAsync();

            var novoProdutoDto = _mapper.Map<ProdutoDto>(novoProduto);

            return new CreatedAtRouteResult("ObterProduto",
                   new { id = novoProdutoDto.ProdutoId }, novoProdutoDto);
        }


        [HttpPut("{id:int}")]

        public async Task<ActionResult<ProdutoDto>> Put(int id, ProdutoDto produtoDto)
        {
            if (id != produtoDto.ProdutoId)
            {
                //_logger.LogWarning("Dados inválidos...");
                return BadRequest("Dados inválidos...");
            }

            var produto = _mapper.Map<Produto>(produtoDto);

            var produtoAtualizado = _unitOfWork.ProdutoRepository.Update(produto);
            await _unitOfWork.CommitAsync();

            var produtoAtualizadoDto = _mapper.Map<ProdutoDto>(produtoAtualizado);

            return Ok(produtoAtualizadoDto);


        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult<ProdutoDto>> Delete(int id)
        {
            var produto = await _unitOfWork.ProdutoRepository.GetAsync(p => p.ProdutoId == id);
            if (produto == null)
            {
                return NotFound("Produto não encontrado...");
            }

            var produtoDeletado = _unitOfWork.ProdutoRepository.Delete(produto);
            await _unitOfWork.CommitAsync();

            var produtoDeletadoDto = _mapper.Map<ProdutoDto>(produtoDeletado);
            return Ok(produtoDeletadoDto);


        }

    }

}

