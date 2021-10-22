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

const routes: Routes = [
  {path: "tipopagar", component:TipopagarComponent},
  {path: "pagar", component: PagarComponent},
  {path: "saldoatualizado", component: SaldoatualizadoComponent},
  {path: "contacaixa", component: ContacaixaComponent},
  {path: "carteira", component: CarteiraComponent},
  {path: "movcaixa", component: MovcaixaComponent},
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
  {path: "cliente/incluir", component: ClienteIncluirComponent}, 
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
