using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PropEase.API.Models;
using PropEase.API.Services;
using PropEase.API.DTOs;

namespace PropEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProprietariosController : ControllerBase
    {
        private readonly ProprietarioService _service;
        private readonly IMapper _mapper;
        public ProprietariosController(ProprietarioService service, IMapper mapper) { _service = service; _mapper = mapper; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var proprietarios = await _service.GetAllAsync();
            var dtos = proprietarios.Select(p => _mapper.Map<ProprietarioDto>(p)).ToList();
            return Ok(new { Mensagem = dtos.Any() ? "Proprietários encontrados." : "Nenhum proprietário cadastrado ainda.", Dados = dtos });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _service.GetByIdAsync(id);
            return p is null ? NotFound($"Proprietário {id} não encontrado.") : Ok(_mapper.Map<ProprietarioDto>(p));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProprietarioDto dto)
        {
            if (dto is null) return BadRequest("Dados inválidos.");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entity = _mapper.Map<Proprietario>(dto);
            var novo = await _service.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = novo.Id }, new { Mensagem = "Proprietário criado com sucesso.", Dados = _mapper.Map<ProprietarioDto>(novo) });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateProprietarioDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entity = _mapper.Map<Proprietario>(dto);
            var ok = await _service.UpdateAsync(id, entity);
            return ok ? Ok(new { Mensagem = "Proprietário atualizado com sucesso." }) : NotFound($"Proprietário {id} não encontrado.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? Ok(new { Mensagem = "Proprietário removido com sucesso." }) : NotFound($"Proprietário {id} não encontrado.");
        }
    }
}
