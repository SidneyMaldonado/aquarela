import { ContaCaixa } from './entities/contacaixa';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContacaixaComponent } from './components/contacaixa/contacaixa.component';
import { CarteiraComponent } from './components/carteira/carteira.component';
import { MovcaixaComponent } from './components/movcaixa/movcaixa.component';

const routes: Routes = [
  {path: "contacaixa", component: ContacaixaComponent},
  {path: "carteira", component: CarteiraComponent},
  {path: "movcaixa", component: MovcaixaComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
