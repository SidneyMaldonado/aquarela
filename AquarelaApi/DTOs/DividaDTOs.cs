namespace AquarelaApi.DTOs;

public record CreateDividaRequest(
    int IdUsuario,
    string NmDivida,
    int DiaVencimento,
    DateTime DtPrimeiroVencimento,
    int NrParcelas,
    decimal NrValor
);

public record UpdateDividaRequest(
    int IdDivida,
    int IdUsuario,
    string NmDivida,
    int DiaVencimento,
    DateTime DtPrimeiroVencimento,
    int NrParcelas,
    decimal NrValor
);

public record DividaResponse(
    int IdDivida,
    int IdUsuario,
    string NmDivida,
    int DiaVencimento,
    DateTime DtPrimeiroVencimento,
    int NrParcelas,
    decimal NrValor,
    string? NmUsuario
);
