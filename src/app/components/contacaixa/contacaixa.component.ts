import { ContaCaixaLista } from './../../entities/contacaixaLista';
import { ContaCaixa } from './../../entities/contacaixa';
import { Component, OnInit } from '@angular/core';
import { ContaCaixaService } from 'src/app/services/contacaixa.service';

@Component({
  selector: 'app-contacaixa',
  templateUrl: './contacaixa.component.html',
  styleUrls: ['./contacaixa.component.css']
})
export class ContacaixaComponent implements OnInit {

 contacaixas: ContaCaixaLista[] = [];

  constructor(private contaCaixaService: ContaCaixaService) { }

  ngOnInit(): void {


    this.contaCaixaService.listar().subscribe( 

      dados => this.contacaixas = dados,
      error => alert("Erro ao buscar conta caixa")
    );

  }

}
