import { Component, OnInit } from '@angular/core';
import { GrupoProduto } from 'src/app/entities/grupoproduto';
import { GrupoProdutoService } from 'src/app/services/grupoproduto.service';

@Component({
  selector: 'app-grupoproduto',
  templateUrl: './grupoproduto.component.html',
  styleUrls: ['./grupoproduto.component.css']
})
export class GrupoProdutoComponent implements OnInit {

  public gruposProduto: GrupoProduto[] = []

  constructor(private grupoProdutoService: GrupoProdutoService) { }

  ngOnInit(): void {
    this.grupoProdutoService.listar().subscribe(resp => this.gruposProduto = resp, error => console.log(error))
  }

}
