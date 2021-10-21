import { Component, OnInit } from '@angular/core';
import { Produto } from 'src/app/entities/produto';
import { ProdutoService } from 'src/app/services/produto.service';

@Component({
  selector: 'app-produto',
  templateUrl: './produto.component.html',
  styleUrls: ['./produto.component.css']
})
export class ProdutoComponent implements OnInit {

  public produtos: Produto[] = []

  constructor(private produtoService: ProdutoService) { }

  ngOnInit(): void {
    this.produtoService.listar().subscribe(resp => {this.produtos = resp; console.log(resp)}, error => console.log(error))
  }

}
