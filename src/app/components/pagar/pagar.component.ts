import { Component, OnInit } from '@angular/core';
import { Pagar } from 'src/app/entities/pagar';
import { PagarLista } from 'src/app/entities/pagarLista';
import { PagarService } from 'src/app/services/pagar.service';

@Component({
  selector: 'app-pagar',
  templateUrl: './pagar.component.html',
  styleUrls: ['./pagar.component.css']
})
export class PagarComponent implements OnInit {

  pagars: Pagar[] = [];
  
  constructor(private pagarService: PagarService) { }

  ngOnInit(): void {

    this.pagarService.listar().subscribe(
      dados => this.pagars = dados,
      error => alert("Erro ao buscar *Pagar*")
    )
  }

}
