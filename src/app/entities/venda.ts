export interface Venda {
    idVenda: number,
    idFornecedor: number,
    idCliente: number,
    cdUsuario: number,
    nrTotal: number,
    nrDesconto: number,
    nrPagar: number,
    dmPago: false,
    dmAtivo: true,
    dtVenda: Date
}