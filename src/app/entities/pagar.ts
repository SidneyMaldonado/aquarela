export interface Pagar{
    idPagar: number;
    idTipoPagar: number;
    idFornecedor: number;
    nmPagar: string;
    qtdParcela: number;
    dtEmissao: Date;
    nrValor: number;
    dmSituacao: boolean;
    dtPrimeiroVencimento: Date;
}