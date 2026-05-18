namespace AquarelaApi.DTOs;

public record CreateUsuarioRequest(
    string NmUsuario,
    string DsEmail,
    string DsSenha,
    bool DmAtivo = true
);

public record UpdateUsuarioRequest(
    int IdUsuario,
    string NmUsuario,
    string DsEmail,
    bool DmAtivo
    // Senha não é atualizada por este endpoint
);

public record UsuarioResponse(
    int IdUsuario,
    string NmUsuario,
    string DsEmail,
    bool DmAtivo
    // Senha NUNCA deve ser retornada
);
