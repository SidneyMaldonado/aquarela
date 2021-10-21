import { ParcelaReceber } from './../../entities/parcelareceber';
import { Component, OnInit } from '@angular/core';
import { ParcelaReceberService } from 'src/app/services/parelareceber.service';


@Component({
  selector: 'app-parcelareceber',
  templateUrl: './parcelareceber.component.html',
  styleUrls: ['./parcelareceber.component.css']
})
export class ParcelaReceberComponent implements OnInit {

 parcelasreceber: ParcelaReceber[] = [];

  constructor(private parcelaReceberService: ParcelaReceberService) { }

  ngOnInit(): void {


    this.parcelaReceberService.listar().subscribe( 

      dados => this.parcelasreceber = dados,
      error => alert("Erro ao buscar conta caixa")
    );

  }

}
