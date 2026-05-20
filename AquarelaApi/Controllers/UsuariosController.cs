using AquarelaApi.DTOs;
using AquarelaApi.Models;
using AquarelaApi.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquarelaApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioUseCase _useCase;

    public UsuariosController(UsuarioUseCase useCase) => _useCase = useCase;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _useCase.GetAllAsync();
        var response = usuarios.Select(u => new UsuarioResponse(
            u.IdUsuario,
            u.NmUsuario,
            u.DsEmail,
            u.DmAtivo
        ));
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var usuario = await _useCase.GetByIdAsync(id);
        if (usuario is null) return NotFound();

        var response = new UsuarioResponse(
            usuario.IdUsuario,
            usuario.NmUsuario,
            usuario.DsEmail,
            usuario.DmAtivo
        );
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUsuarioRequest request)
    {
        var usuario = new Usuario
        {
            NmUsuario = request.NmUsuario,
            DsEmail = request.DsEmail,
            DsSenha = request.DsSenha,
            DmAtivo = request.DmAtivo
        };

        var created = await _useCase.CreateAsync(usuario);

        var response = new UsuarioResponse(
            created.IdUsuario,
            created.NmUsuario,
            created.DsEmail,
            created.DmAtivo
        );

        return CreatedAtAction(nameof(GetById), new { id = created.IdUsuario }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUsuarioRequest request)
    {
        if (id != request.IdUsuario) return BadRequest();

        // Buscar o usuário existente para manter a senha
        var existing = await _useCase.GetByIdAsync(id);
        if (existing is null) return NotFound();

        existing.NmUsuario = request.NmUsuario;
        existing.DsEmail = request.DsEmail;
        existing.DmAtivo = request.DmAtivo;
        var updated = await _useCase.UpdateAsync(existing);

        var response = new UsuarioResponse(
            updated.IdUsuario,
            updated.NmUsuario,
            updated.DsEmail,
            updated.DmAtivo
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
