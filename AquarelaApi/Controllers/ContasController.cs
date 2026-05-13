using AquarelaApi.Models;
using AquarelaApi.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquarelaApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ContasController : ControllerBase
{
    private readonly ContaUseCase _useCase;

    public ContasController(ContaUseCase useCase) => _useCase = useCase;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _useCase.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var conta = await _useCase.GetByIdAsync(id);
        return conta is null ? NotFound() : Ok(conta);
    }

    [HttpGet("usuario/{idUsuario:int}")]
    public async Task<IActionResult> GetByUsuario(int idUsuario)
        => Ok(await _useCase.GetByUsuarioIdAsync(idUsuario));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Conta conta)
    {
        var created = await _useCase.CreateAsync(conta);
        return CreatedAtAction(nameof(GetById), new { id = created.IdConta }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Conta conta)
    {
        if (id != conta.IdConta) return BadRequest();
        return Ok(await _useCase.UpdateAsync(conta));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _useCase.DeleteAsync(id);
        return NoContent();
    }
}
