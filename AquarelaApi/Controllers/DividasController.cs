using AquarelaApi.DTOs;
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
    public async Task<IActionResult> GetAll()
    {
        var dividas = await _useCase.GetAllAsync();
        var response = dividas.Select(d => new DividaResponse(
            d.IdDivida,
            d.IdUsuario,
            d.NmDivida,
            d.DiaVencimento,
            d.DtPrimeiroVencimento,
            d.NrParcelas,
            d.NrValor,
            d.Usuario?.NmUsuario
        ));
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var divida = await _useCase.GetByIdAsync(id);
        if (divida is null) return NotFound();

        var response = new DividaResponse(
            divida.IdDivida,
            divida.IdUsuario,
            divida.NmDivida,
            divida.DiaVencimento,
            divida.DtPrimeiroVencimento,
            divida.NrParcelas,
            divida.NrValor,
            divida.Usuario?.NmUsuario
        );
        return Ok(response);
    }

    [HttpGet("usuario/{idUsuario:int}")]
    public async Task<IActionResult> GetByUsuario(int idUsuario)
    {
        var dividas = await _useCase.GetByUsuarioIdAsync(idUsuario);
        var response = dividas.Select(d => new DividaResponse(
            d.IdDivida,
            d.IdUsuario,
            d.NmDivida,
            d.DiaVencimento,
            d.DtPrimeiroVencimento,
            d.NrParcelas,
            d.NrValor,
            d.Usuario?.NmUsuario
        ));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDividaRequest request)
    {
        var divida = new Divida
        {
            IdUsuario = request.IdUsuario,
            NmDivida = request.NmDivida,
            DiaVencimento = request.DiaVencimento,
            DtPrimeiroVencimento = request.DtPrimeiroVencimento,
            NrParcelas = request.NrParcelas,
            NrValor = request.NrValor
        };

        var created = await _useCase.CreateAsync(divida);

        var response = new DividaResponse(
            created.IdDivida,
            created.IdUsuario,
            created.NmDivida,
            created.DiaVencimento,
            created.DtPrimeiroVencimento,
            created.NrParcelas,
            created.NrValor,
            null
        );

        return CreatedAtAction(nameof(GetById), new { id = created.IdDivida }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDividaRequest request)
    {
        if (id != request.IdDivida) return BadRequest();

        var divida = new Divida
        {
            IdDivida = request.IdDivida,
            IdUsuario = request.IdUsuario,
            NmDivida = request.NmDivida,
            DiaVencimento = request.DiaVencimento,
            DtPrimeiroVencimento = request.DtPrimeiroVencimento,
            NrParcelas = request.NrParcelas,
            NrValor = request.NrValor
        };

        var updated = await _useCase.UpdateAsync(divida);

        var response = new DividaResponse(
            updated.IdDivida,
            updated.IdUsuario,
            updated.NmDivida,
            updated.DiaVencimento,
            updated.DtPrimeiroVencimento,
            updated.NrParcelas,
            updated.NrValor,
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
