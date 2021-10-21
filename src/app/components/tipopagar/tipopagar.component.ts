import { Component, OnInit } from '@angular/core';
import { TipoPagar } from 'src/app/entities/tipoPagar';
import { TipopagarService } from 'src/app/services/tipopagar.service';

@Component({
  selector: 'app-tipopagar',
  templateUrl: './tipopagar.component.html',
  styleUrls: ['./tipopagar.component.css']
})
export class TipopagarComponent implements OnInit {

  tipopagas: TipoPagar[] = [];
  constructor(private tipoPagarService: TipopagarService) { }

  ngOnInit(): void {
    this.tipoPagarService.listar().subscribe( 

      dados => this.tipopagas = dados,
      error => alert("Erro ao buscar conta caixa")
    );
  }

}
