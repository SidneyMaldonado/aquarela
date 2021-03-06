import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { ContacaixaComponent } from './components/contacaixa/contacaixa.component';
import { PagarComponent } from './components/pagar/pagar.component';
import { SaldoatualizadoComponent } from './components/saldoatualizado/saldoatualizado.component'
import { CarteiraComponent } from './components/carteira/carteira.component';
import { MovcaixaComponent } from './components/movcaixa/movcaixa.component'
import { ReceberComponent } from './components/receber/receber.component';
import { MovdiaComponent } from './components/movdia/movdia.component'
import { ParcelaPagarComponent } from './components/parcelapagar/parcelapagar.component'
import { FornecedorComponent } from './components/fornecedor/fornecedor.component';
import { GrupoProdutoComponent } from './components/grupoproduto/grupoproduto.component'
import { ClienteComponent } from './components/cliente/cliente.component'
import { ClienteService } from './services/cliente.service';
import { ProdutoComponent } from './components/produto/produto.component';
import { VendaitemComponent } from './components/vendaitem/vendaitem.component';
import { ParcelaReceberComponent } from './components/parcelareceber/parcelareceber.component';
import { TipopagarComponent } from './components/tipopagar/tipopagar.component';
import { VendaComponent } from './components/venda/venda.component';
import { ClienteIncluirComponent } from './components/cliente-incluir/cliente-incluir.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MovdiaIncluirComponent } from './components/movdia-incluir/movdia-incluir.component';
import { FornecedorIncluirComponent } from './components/fornecedor-incluir/fornecedor-incluir.component';
import { ProdutoIncluirComponent } from './components/produto-incluir/produto-incluir.component';
import { PagarIncluirComponent } from './components/pagar-incluir/pagar-incluir.component';
import { ContaCaixaInserirComponent } from './components/conta-caixa-inserir/conta-caixa-inserir.component';
import { CarteiraIncluirComponent } from './components/carteira-incluir/carteira-incluir.component';
import { ParcelapagarIncluirComponent } from './components/parcelapagar-incluir/parcelapagar-incluir.component';
import { GrupoprodutoIncluirComponent } from './components/grupoproduto-incluir/grupoproduto-incluir.component';
import { SaldoatualizadoIncluirComponent } from './components/saldoatualizado-incluir/saldoatualizado-incluir.component';
import { TipoPagarIncluirComponent } from './components/tipo-pagar-incluir/tipo-pagar-incluir.component';

@NgModule({
  declarations: [
    AppComponent,
    ContacaixaComponent,
    PagarComponent,
    SaldoatualizadoComponent,
    CarteiraComponent,
    MovcaixaComponent,
    ReceberComponent,
    MovdiaComponent,
    ParcelaPagarComponent,
    FornecedorComponent,
    GrupoProdutoComponent,
    ClienteComponent,
    ProdutoComponent,
    VendaitemComponent,
    ParcelaReceberComponent,
    TipopagarComponent,
    VendaComponent,
    ClienteIncluirComponent,
    FornecedorIncluirComponent,
    PagarIncluirComponent,
    CarteiraIncluirComponent,
    MovdiaIncluirComponent,
    FornecedorIncluirComponent,
    ParcelapagarIncluirComponent,
    ProdutoIncluirComponent,
    GrupoprodutoIncluirComponent,
    ContaCaixaInserirComponent,
    SaldoatualizadoIncluirComponent,
    TipoPagarIncluirComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    ClienteService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
