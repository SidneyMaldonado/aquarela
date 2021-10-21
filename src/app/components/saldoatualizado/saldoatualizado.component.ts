import { Component, OnInit } from '@angular/core';
import { SaldoAtualizadoLista } from 'src/app/entities/saldoAtualizadoLista';
import { SaldoatualizadoService } from 'src/app/services/saldoatualizado.service';

@Component({
  selector: 'app-saldoatualizado',
  templateUrl: './saldoatualizado.component.html',
  styleUrls: ['./saldoatualizado.component.css']
})
export class SaldoatualizadoComponent implements OnInit {

  saldoatualizados: SaldoAtualizadoLista[] = [];

  constructor(private saldoatualizadoService: SaldoatualizadoService) { }

  ngOnInit(): void {

    this.saldoatualizadoService.listar().subscribe(
      dados => this.saldoatualizados = dados,
      error => alert("Erro ao buscar *Saldo Atualizado*")
    );
  }

}
