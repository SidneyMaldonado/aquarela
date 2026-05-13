using AquarelaApi.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace AquarelaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly LoginUseCase _loginUseCase;

    public AuthController(LoginUseCase loginUseCase) => _loginUseCase = loginUseCase;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _loginUseCase.ExecuteAsync(request.Email, request.Senha);

        if (token is null)
            return Unauthorized(new { message = "Email ou senha inválidos." });

        return Ok(new { token });
    }
}

public record LoginRequest(string Email, string Senha);
