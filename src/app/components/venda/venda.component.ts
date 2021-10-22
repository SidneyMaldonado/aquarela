import { Component, OnInit } from '@angular/core';
import { Venda } from 'src/app/entities/venda';
import { VendaService } from 'src/app/services/venda.service';

@Component({
  selector: 'app-venda',
  templateUrl: './venda.component.html',
  styleUrls: ['./venda.component.css']
})
export class VendaComponent implements OnInit {

  vendas: Venda[] = []

  constructor(private vendaService: VendaService) { }

  ngOnInit(): void {
    this.vendaService.listar().subscribe(res => this.vendas = res, error => console.log(`ERROR: ${error}`))
  }

}
