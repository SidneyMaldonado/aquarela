import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { ContacaixaComponent } from './components/contacaixa/contacaixa.component';
import { ReceberComponent } from './components/receber/receber.component';
import { MovdiaComponent } from './components/movdia/movdia.component'
import { ParcelaPagarComponent } from './components/parcelapagar/parcelapagar.component'
import { FornecedorComponent } from './components/fornecedor/fornecedor.component';
import { GrupoProdutoComponent } from './components/grupoproduto/grupoproduto.component'
import { ClienteComponent } from './components/cliente/cliente.component'
import { ClienteService } from './services/cliente.service';
import { ParcelaReceberComponent } from './components/parcelareceber/parcelareceber.component';

@NgModule({
  declarations: [
    AppComponent,
    ContacaixaComponent,
    ReceberComponent,
    MovdiaComponent,
    ParcelaPagarComponent,
    FornecedorComponent,
    GrupoProdutoComponent,
    ClienteComponent,
    ParcelaReceberComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    ClienteService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
