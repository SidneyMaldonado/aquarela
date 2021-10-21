import { ContaCaixa } from './entities/contacaixa';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContacaixaComponent } from './components/contacaixa/contacaixa.component';

const routes: Routes = [
  {path: "contacaixa", component: ContacaixaComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
