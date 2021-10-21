import { Receber } from "./receber";

export interface ReceberLista{
    idReceber?:number,
    idFornecedor:number,
    nmReceber:string,
    qtdParcela:number,
    dtEmissao:Date,
    nrValor:number,
    primeiroVencimento:Date,
    dmSituacao:boolean
    nomeFornecedor:String
}