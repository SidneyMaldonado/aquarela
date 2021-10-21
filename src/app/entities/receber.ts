import { DecimalPipe } from "@angular/common";

export interface Receber{
    idReceber?:number,
    idFornecedor:number,
    nmReceber:string,
    qtdParcela:number,
    dtEmissao:Date,
    nrValor:number,
    primeiroVencimento:Date,
    dmSituacao:boolean
}