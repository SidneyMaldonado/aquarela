export interface PagarLista{
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