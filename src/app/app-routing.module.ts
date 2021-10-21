import { ContaCaixa } from './entities/contacaixa';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContacaixaComponent } from './components/contacaixa/contacaixa.component';
import { ReceberComponent } from './components/receber/receber.component';
import { MovdiaComponent } from './components/movdia/movdia.component';
import { ParcelaPagarComponent } from './components/parcelapagar/parcelapagar.component';
import { FornecedorComponent } from './components/fornecedor/fornecedor.component';
import { GrupoProdutoComponent } from './components/grupoproduto/grupoproduto.component';
import { ClienteComponent } from './components/cliente/cliente.component';
import { VendaitemComponent } from './components/vendaitem/vendaitem.component';



const routes: Routes = [
  { path: "contacaixa", component: ContacaixaComponent },
  { path: "contacaixa", component: ContacaixaComponent },
  { path: "receber", component: ReceberComponent },
  { path: "movdia", component: MovdiaComponent },
  { path: "parcelapagar", component: ParcelaPagarComponent },
  { path: "contacaixa", component: ContacaixaComponent },
  { path: "fornecedor", component: FornecedorComponent },
  { path: "grupoproduto", component: GrupoProdutoComponent },
  { path: "contacaixa", component: ContacaixaComponent },
  { path: "cliente", component: ClienteComponent },
  { path: "vendaitem", component: VendaitemComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
