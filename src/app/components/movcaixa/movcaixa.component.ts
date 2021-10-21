import { Component, OnInit } from '@angular/core';
import { Movcaixa } from 'src/app/entities/movcaixa';
import { MovcaixaService } from 'src/app/services/movcaixa.service';

@Component({
  selector: 'app-movcaixa',
  templateUrl: './movcaixa.component.html',
  styleUrls: ['./movcaixa.component.css']
})
export class MovcaixaComponent implements OnInit {

  movcaixas: Movcaixa[] = [];

  constructor(private movcaixaService: MovcaixaService) { }

  ngOnInit(): void {
    this.movcaixaService.listar().subscribe( 

      dados => this.movcaixas = dados,
      error => alert("Erro ao buscar conta caixa")
    );
  }

}
