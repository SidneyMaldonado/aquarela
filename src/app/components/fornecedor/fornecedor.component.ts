import { Component, OnInit } from '@angular/core';
import { Fornecedor } from 'src/app/entities/fornecedor';
import { FornecedorService } from 'src/app/services/fornecedor.service';

@Component({
  selector: 'app-fornecedor',
  templateUrl: './fornecedor.component.html',
  styleUrls: ['./fornecedor.component.css']
})
export class FornecedorComponent implements OnInit {

  fornecedores: Fornecedor[] = []

  constructor(private fornecedorService: FornecedorService) { 
  }

  ngOnInit(): void {
    this.fornecedorService.listar().subscribe(resp => this.fornecedores = resp, error => console.log(error))
  }

}
