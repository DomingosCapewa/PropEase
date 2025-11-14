using Microsoft.AspNetCore.Mvc;
using PropEase.API.Models;
using PropEase.API.Services;
using PropEase.API.DTOs;
using System.Linq;

namespace PropEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImoveisController : ControllerBase
    {
        private readonly ImovelService _service;
        private readonly ProprietarioService _proprietarios;
        private readonly AutoMapper.IMapper _mapper;

        public ImoveisController(ImovelService service, ProprietarioService proprietarios, AutoMapper.IMapper mapper)
        {
            _service = service;
            _proprietarios = proprietarios;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var imoveis = await _service.GetAllAsync();
            var dtos = imoveis.Select(i => _mapper.Map<ImovelDto>(i)).ToList();
            return Ok(new { Mensagem = dtos.Any() ? "Imóveis encontrados." : "Nenhum imóvel cadastrado ainda.", Dados = dtos });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest("ID inválido. Deve ser maior que zero.");

            var imovel = await _service.GetByIdAsync(id);
            if (imovel is null) return NotFound("Imóvel não encontrado.");

            return Ok(new { Mensagem = "Imóvel encontrado.", Dados = _mapper.Map<ImovelDto>(imovel) });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateImovelDto dto)
        {
            if (dto is null) return BadRequest("Payload inválido");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var imovel = _mapper.Map<Imovel?>(dto);
            if (imovel is null)
                return BadRequest("Tipo inválido. Use 'Casa' ou 'Apartamento'.");

            var owner = await _proprietarios.GetByIdAsync(dto.ProprietarioId);
            if (owner is null)
                return BadRequest($"Proprietário {dto.ProprietarioId} não encontrado. Crie-o antes de vincular ao imóvel.");

            var criado = await _service.AddAsync(imovel);
            var carregado = await _service.GetByIdAsync(criado.Id);

            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, new { Mensagem = "Imóvel criado com sucesso.", Dados = _mapper.Map<ImovelDto>(criado) });
        }

        [HttpPost("alugar/{id}")]
        public async Task<IActionResult> Alugar(int id)
        {
            var result = await _service.AlugarAsync(id);
            return result ? Ok(new { Mensagem = "Imóvel alugado com sucesso." }) : NotFound("Imóvel não encontrado.");
        }

        [HttpPut("disponibilizar/{id}")]
        public async Task<IActionResult> Disponibilizar(int id)
        {
            
            var result = await _service.DisponibilizarAsync(id);
            return result ? Ok(new { Mensagem = "Imóvel agora está disponível." }) : NotFound("Imóvel não encontrado.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? Ok(new { Mensagem = "Imóvel removido com sucesso." }) : NotFound("Imóvel não encontrado.");
        }

        [HttpGet("{id}/calcular-aluguel")]
        public async Task<IActionResult> CalcularAluguel(int id, [FromQuery] int periodo)
        {
            if (periodo <= 0) return BadRequest("Periodo deve ser maior que zero.");

            var imovel = await _service.GetByIdAsync(id);
            if (imovel is null) return NotFound("Imóvel não encontrado");

            var total = imovel.CalcularAluguel(periodo);
            return Ok(new { Id = id, Periodo = periodo, Total = total });
        }
    }
}
