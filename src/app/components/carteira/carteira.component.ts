import { Component, OnInit } from '@angular/core';
import { Carteira } from 'src/app/entities/carteira';
import { CarteiraService } from 'src/app/services/carteira.service';

@Component({
  selector: 'app-carteira',
  templateUrl: './carteira.component.html',
  styleUrls: ['./carteira.component.css']
})
export class CarteiraComponent implements OnInit {

  carteiras: Carteira[] = [];

  constructor(private carteiraService: CarteiraService) { }
 

  ngOnInit(): void {
    this.carteiraService.listar().subscribe( 

      dados => this.carteiras = dados,
      error => alert("Erro ao buscar conta caixa")
    );
  }

}
