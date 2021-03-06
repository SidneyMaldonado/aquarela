
import { ContaCaixa } from './entities/contacaixa';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContacaixaComponent } from './components/contacaixa/contacaixa.component';
import { PagarComponent } from './components/pagar/pagar.component';


import { CarteiraComponent } from './components/carteira/carteira.component';
import { MovcaixaComponent } from './components/movcaixa/movcaixa.component';

import { ReceberComponent } from './components/receber/receber.component';
import { MovdiaComponent } from './components/movdia/movdia.component';
import { ParcelaPagarComponent } from './components/parcelapagar/parcelapagar.component';
import { FornecedorComponent } from './components/fornecedor/fornecedor.component';
import { GrupoProdutoComponent } from './components/grupoproduto/grupoproduto.component';
import { ClienteComponent } from './components/cliente/cliente.component';
import { ProdutoComponent } from './components/produto/produto.component';
import { VendaitemComponent } from './components/vendaitem/vendaitem.component';
import { ParcelaReceberComponent } from './components/parcelareceber/parcelareceber.component';
import { SaldoatualizadoComponent } from './components/saldoatualizado/saldoatualizado.component';
import { TipopagarComponent } from './components/tipopagar/tipopagar.component';
import { VendaComponent } from './components/venda/venda.component';
import { ClienteIncluirComponent } from './components/cliente-incluir/cliente-incluir.component';
import { MovdiaIncluirComponent } from './components/movdia-incluir/movdia-incluir.component';
import { FornecedorIncluirComponent } from './components/fornecedor-incluir/fornecedor-incluir.component';
import { ProdutoIncluirComponent } from './components/produto-incluir/produto-incluir.component';
import { PagarIncluirComponent } from './components/pagar-incluir/pagar-incluir.component';
import { ContaCaixaInserirComponent } from './components/conta-caixa-inserir/conta-caixa-inserir.component';
import { CarteiraIncluirComponent } from './components/carteira-incluir/carteira-incluir.component';
import { ParcelapagarIncluirComponent } from './components/parcelapagar-incluir/parcelapagar-incluir.component';
import { TipoPagarIncluirComponent } from './components/tipo-pagar-incluir/tipo-pagar-incluir.component';
import { GrupoprodutoIncluirComponent } from './components/grupoproduto-incluir/grupoproduto-incluir.component';
import { SaldoatualizadoIncluirComponent } from './components/saldoatualizado-incluir/saldoatualizado-incluir.component';



const routes: Routes = [
  {path: "tipopagar", component:TipopagarComponent},
  {path: "pagar", component: PagarComponent},
  {path: "saldoatualizado", component: SaldoatualizadoComponent},
  {path: "contacaixa", component: ContacaixaComponent},
  {path: "carteira", component: CarteiraComponent},
  {path: "movcaixa", component: MovcaixaComponent},
  {path: "movdia", component: MovdiaComponent},
  {path: "vendaitem", component: VendaitemComponent},
  {path: "receber", component: ReceberComponent},
  {path: "contacaixa", component: ContacaixaComponent},
  {path: "fornecedor", component: FornecedorComponent},
  {path: "grupoproduto", component: GrupoProdutoComponent},
  {path: "contacaixa", component: ContacaixaComponent},
  {path: "cliente", component: ClienteComponent},
  {path: "produto", component: ProdutoComponent},
  {path: "contacaixa", component: ContacaixaComponent},
  {path: "parcelapagar", component: ParcelaPagarComponent},
  {path: "parcelareceber", component: ParcelaReceberComponent},
  {path: "contacaixa", component: ContacaixaComponent},
  {path: "fornecedor", component: FornecedorComponent},
  {path: "grupoproduto", component: GrupoProdutoComponent},
  {path: "contacaixa", component: ContacaixaComponent},
  {path: "cliente", component: ClienteComponent},  
  {path: "venda", component: VendaComponent},
  {path: "carteira/incluir", component: CarteiraIncluirComponent},
  {path: "parcelapagar/incluir", component: ParcelapagarIncluirComponent},
  {path: "cliente/incluir", component: ClienteIncluirComponent}, 
  {path: "produto/incluir", component: ProdutoIncluirComponent}, 
  {path: "fornecedor/incluir", component: FornecedorIncluirComponent},
  {path: "pagar/incluir", component: PagarIncluirComponent},
  {path: "carteiraincluir", component: CarteiraIncluirComponent},
  {path:"movdia/incluir", component: MovdiaIncluirComponent},
  {path: "fornecedor/incluir", component: FornecedorIncluirComponent},
  {path: "grupoproduto/incluir", component: GrupoprodutoIncluirComponent},
  {path: "contacaixa/incluir", component:ContaCaixaInserirComponent},
  {path: "saldo/incluir", component: SaldoatualizadoIncluirComponent},
  {path:"tipopagar/incluir", component:TipoPagarIncluirComponent}  
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
