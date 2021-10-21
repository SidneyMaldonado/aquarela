import { Component, OnInit } from '@angular/core';
import { PagarLista } from 'src/app/entities/pagarLista';
import { PagarService } from 'src/app/services/pagar.service';

@Component({
  selector: 'app-pagar',
  templateUrl: './pagar.component.html',
  styleUrls: ['./pagar.component.css']
})
export class PagarComponent implements OnInit {

  pagars: PagarLista[] = [];
  
  constructor(private pagarService: PagarService) { }

  ngOnInit(): void {

    this.pagarService.listar().subscribe(
      dados => this.pagars = dados,
      error => alert("Erro ao buscar *Pagar*")
    );
  }

}
