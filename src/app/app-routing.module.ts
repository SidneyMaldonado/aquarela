import { ContaCaixa } from './entities/contacaixa';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContacaixaComponent } from './components/contacaixa/contacaixa.component';
import { PagarComponent } from './components/pagar/pagar.component';

const routes: Routes = [
  {path: "contacaixa", component: ContacaixaComponent},
  {path: "pagar", component: PagarComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
