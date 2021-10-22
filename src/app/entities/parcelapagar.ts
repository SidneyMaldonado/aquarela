export interface ParcelaPagar{
    idParcela: number,
    nrParcela: number,
    dsDocumento: string,
    dtVencimento: Date,
    nrValor: number,
    dtPagamento: Date,
    nrValorPago: number,
    idPagar: number,
    dmSituacao: boolean,
    idContaCaixa: number
    }