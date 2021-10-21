import { ParcelaPagar } from './../../entities/parcelapagar';
import { Component, OnInit } from '@angular/core';
import { ParcelaPagarService } from 'src/app/services/parcelapagar.service';

@Component({
  selector: 'app-parcelapagar',
  templateUrl: './parcelapagar.component.html',
  styleUrls: ['./parcelapagar.component.css']
})
export class ParcelaPagarComponent implements OnInit {

 parcelapagars: ParcelaPagar[] = [];

  constructor(private parcelaPagarService: ParcelaPagarService) { }

  ngOnInit(): void {


    this.parcelaPagarService.listar().subscribe( 

      dados => this.parcelapagars = dados,
      error => alert("Erro ao buscar conta caixa")
    );

  }

}