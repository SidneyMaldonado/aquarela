import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { ContacaixaComponent } from './components/contacaixa/contacaixa.component';
import { ParcelaPagarComponent } from './components/parcelapagar/parcelapagar.component'

@NgModule({
  declarations: [
    AppComponent,
    ContacaixaComponent,
    ParcelaPagarComponent
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
