using AquarelaApi.DTOs;
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
    public async Task<IActionResult> GetAll()
    {
        var contas = await _useCase.GetAllAsync();
        var response = contas.Select(c => new ContaResponse(
            c.IdConta,
            c.IdUsuario,
            c.NmConta,
            c.NrSaldo,
            c.Usuario?.NmUsuario
        ));
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var conta = await _useCase.GetByIdAsync(id);
        if (conta is null) return NotFound();

        var response = new ContaResponse(
            conta.IdConta,
            conta.IdUsuario,
            conta.NmConta,
            conta.NrSaldo,
            conta.Usuario?.NmUsuario
        );
        return Ok(response);
    }

    [HttpGet("usuario/{idUsuario:int}")]
    public async Task<IActionResult> GetByUsuario(int idUsuario)
    {
        var contas = await _useCase.GetByUsuarioIdAsync(idUsuario);
        var response = contas.Select(c => new ContaResponse(
            c.IdConta,
            c.IdUsuario,
            c.NmConta,
            c.NrSaldo,
            c.Usuario?.NmUsuario
        ));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContaRequest request)
    {
        var conta = new Conta
        {
            IdUsuario = request.IdUsuario,
            NmConta = request.NmConta,
            NrSaldo = request.NrSaldo
        };

        var created = await _useCase.CreateAsync(conta);

        var response = new ContaResponse(
            created.IdConta,
            created.IdUsuario,
            created.NmConta,
            created.NrSaldo,
            null
        );

        return CreatedAtAction(nameof(GetById), new { id = created.IdConta }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateContaRequest request)
    {
        if (id != request.IdConta) return BadRequest();

        var conta = new Conta
        {
            IdConta = request.IdConta,
            IdUsuario = request.IdUsuario,
            NmConta = request.NmConta,
            NrSaldo = request.NrSaldo
        };

        var updated = await _useCase.UpdateAsync(conta);

        var response = new ContaResponse(
            updated.IdConta,
            updated.IdUsuario,
            updated.NmConta,
            updated.NrSaldo,
            null
        );

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _useCase.DeleteAsync(id);
        return NoContent();
    }
}