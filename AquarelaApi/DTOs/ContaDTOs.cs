namespace AquarelaApi.DTOs;

public record CreateContaRequest(
    int IdUsuario,
    string NmConta,
    decimal NrSaldo
);

public record UpdateContaRequest(
    int IdConta,
    int IdUsuario,
    string NmConta,
    decimal NrSaldo
);

public record ContaResponse(
    int IdConta,
    int IdUsuario,
    string NmConta,
    decimal NrSaldo,
    string? NmUsuario  // Nome do usuário (opcional, para exibição)
);
