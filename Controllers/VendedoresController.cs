using AutoMapper;
using LojaAPI.Dtos;
using LojaAPI.DTOs.Mappings;
using LojaAPI.Filters;
using LojaAPI.Models;
using LojaAPI.Pagination;
using LojaAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;

namespace LojaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedoresController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<VendedoresController>? _logger;
        private readonly IMapper _mapper = mapper;

       

        [HttpGet("ListarTodosOsVendedores")]
        public async Task<ActionResult<IEnumerable<VendedorDto>>> Get()
        {
            var vendedores = await _unitOfWork.VendedorRepository.GetAllAsync();

            if (vendedores is null)
                return NotFound("Não existem vendedores...");


            var vendedoresDto = vendedores.ToVendedorDtoList();
            return Ok(vendedoresDto);

        }

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<VendedorDto>>> Get([FromQuery]
                               VendedoresParameters vendedoresParameters)
        {
            var vendedores = await _unitOfWork.VendedorRepository.GetVendedoresAsync(vendedoresParameters);


            return ObterVendedores(vendedores);

        }

        

        [HttpGet("filter/nome/pagination")]
        [ServiceFilter(typeof(ApiLogginFilter))]
        public async Task<ActionResult<IEnumerable<VendedorDto>>> GetVendedoresFilterNome([FromQuery] VendedoresFiltroNome
                                                                                       vendedoresFiltroNome)
        {
            var vendedores = await _unitOfWork.VendedorRepository.GetVendedoresFiltroNomeAsync(vendedoresFiltroNome);


            return ObterVendedores(vendedores);
        }
        private ActionResult<IEnumerable<VendedorDto>> ObterVendedores(IPagedList<Vendedor> vendedors)
        {

            var metadata = new
            {
                vendedors.Count,
                vendedors.PageSize,
                vendedors.PageCount,
                vendedors.TotalItemCount,
                vendedors.HasNextPage,
                vendedors.HasPreviousPage
            };

            Response.Headers.Append("x-Pagination", JsonConvert.SerializeObject(metadata));

            var vendedoresDto = _mapper.Map<IEnumerable<VendedorDto>>(vendedors);

            return Ok(vendedoresDto);

        }


        [HttpGet("BuscarVendedorPorId/{idVendedor}")]

        public async Task<ActionResult<VendedorDto>> BuscarVendedorPorId(int idVendedor)
        {
            var vendedor = await _unitOfWork.VendedorRepository.GetAsync(c => c.IdVendedor == idVendedor);
            if (vendedor is null)
            {
                //_logger.LogWarning($"Vendedor com id = {idVendedor} não encontrado, tente outro id...");
                return NotFound($"Vendedor com id = {idVendedor} não encontrado, tente outro id...");

            }

            var vendedorDto = _mapper.Map<VendedorDto>(vendedor);
            return Ok(vendedorDto);

        }

      
       


        [HttpPost("CriarVendedor")]

        public async Task<ActionResult<VendedorDto>> Post(VendedorDto vendedorDto)
        {
            if (vendedorDto is null)
            {
                //_logger.LogWarning("Dados inválidos...");
                return BadRequest("Dados inválidos...");
            }

            var vendedor = _mapper.Map<Vendedor>(vendedorDto);

            var novoVendedor = _unitOfWork.VendedorRepository.Create(vendedor);
            await _unitOfWork.CommitAsync();

            var novoVendedorDto = _mapper.Map<VendedorDto>(novoVendedor);

            return new CreatedAtRouteResult("ObterVendedor",
                   new { idVendedor = novoVendedorDto.IdVendedor }, novoVendedorDto);

        }




        [HttpPut("{id:int}EditarVendedor")]

        public async Task<ActionResult<VendedorDto>> Put(int id, VendedorDto vendedorDto)
        {
            if (id != vendedorDto.IdVendedor)
            {
                //_logger.LogWarning("Dados inválidos...");
                return BadRequest("Dados inválidos...");
            }

            var vendedor = _mapper.Map<Vendedor>(vendedorDto);

            var vendedorEditado = _unitOfWork.VendedorRepository.Update(vendedor);
            await _unitOfWork.CommitAsync();

            var vendedorEditadoDto = _mapper.Map<VendedorDto>(vendedorEditado);

            return Ok(vendedorEditadoDto);


        }


        [HttpDelete("{idVendedor:int}DeletarVendedor")]

        public async Task<ActionResult<VendedorDto>> Delete(int idVendedor)
        {
            var vendedor = await _unitOfWork.VendedorRepository.GetAsync(p => p.IdVendedor == idVendedor);
            if (vendedor == null)
            {
                return NotFound("Vendedor não encongrado...");
            }

            var vendedorDeletado = _unitOfWork.VendedorRepository.Delete(vendedor);
            await _unitOfWork.CommitAsync();

            var vendedorDeletadoDto = _mapper.Map<VendedorDto>(vendedorDeletado);
            return Ok(vendedorDeletadoDto);
        }

    }
}
