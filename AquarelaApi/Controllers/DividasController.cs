using AquarelaApi.Models;
using AquarelaApi.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquarelaApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DividasController : ControllerBase
{
    private readonly DividaUseCase _useCase;

    public DividasController(DividaUseCase useCase) => _useCase = useCase;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _useCase.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var divida = await _useCase.GetByIdAsync(id);
        return divida is null ? NotFound() : Ok(divida);
    }

    [HttpGet("usuario/{idUsuario:int}")]
    public async Task<IActionResult> GetByUsuario(int idUsuario)
        => Ok(await _useCase.GetByUsuarioIdAsync(idUsuario));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Divida divida)
    {
        var created = await _useCase.CreateAsync(divida);
        return CreatedAtAction(nameof(GetById), new { id = created.IdDivida }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Divida divida)
    {
        if (id != divida.IdDivida) return BadRequest();
        return Ok(await _useCase.UpdateAsync(divida));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _useCase.DeleteAsync(id);
        return NoContent();
    }
}
