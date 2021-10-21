import { ContaCaixa } from './entities/contacaixa';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContacaixaComponent } from './components/contacaixa/contacaixa.component';
import { ParcelaPagarComponent } from './components/parcelapagar/parcelapagar.component';
import { FornecedorComponent } from './components/fornecedor/fornecedor.component';
import { GrupoProdutoComponent } from './components/grupoproduto/grupoproduto.component';
import { ClienteComponent } from './components/cliente/cliente.component';

const routes: Routes = [
  {path: "contacaixa", component: ContacaixaComponent},
  {path: "parcelapagar", component: ParcelaPagarComponent},
  {path: "contacaixa", component: ContacaixaComponent},
  {path: "fornecedor", component: FornecedorComponent},
  {path: "grupoproduto", component: GrupoProdutoComponent},
  {path: "contacaixa", component: ContacaixaComponent},
  {path: "cliente", component: ClienteComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
