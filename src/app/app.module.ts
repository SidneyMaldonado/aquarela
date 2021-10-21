import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { ContacaixaComponent } from './components/contacaixa/contacaixa.component';
import { CarteiraComponent } from './components/carteira/carteira.component';
import { MovcaixaComponent } from './components/movcaixa/movcaixa.component'

@NgModule({
  declarations: [
    AppComponent,
    ContacaixaComponent,
    CarteiraComponent,
    MovcaixaComponent
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
