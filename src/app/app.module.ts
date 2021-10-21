import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { ContacaixaComponent } from './components/contacaixa/contacaixa.component';
import { PagarComponent } from './components/pagar/pagar.component';
import { SaldoatualizadoComponent } from './components/saldoatualizado/saldoatualizado.component'

@NgModule({
  declarations: [
    AppComponent,
    ContacaixaComponent,
    PagarComponent,
    SaldoatualizadoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
