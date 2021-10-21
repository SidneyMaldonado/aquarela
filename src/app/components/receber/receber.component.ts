import { Component, OnInit } from '@angular/core';
import { ReceberLista } from 'src/app/entities/receberLista';
import { ReceberService } from 'src/app/services/receber.service';

@Component({
  selector: 'app-receber',
  templateUrl: './receber.component.html',
  styleUrls: ['./receber.component.css']
})
export class ReceberComponent implements OnInit {
  recebimentos:ReceberLista[]=[]
  constructor(private receberService:ReceberService) { }

  ngOnInit(): void {
    this.receberService.listar().subscribe(
      data=> this.recebimentos = data
    )
  }

}
