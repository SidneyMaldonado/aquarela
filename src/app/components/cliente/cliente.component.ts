import { Component, OnInit } from '@angular/core';
import { Cliente } from 'src/app/entities/cliente';
import { ClienteService } from 'src/app/services/cliente.service';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit {

  clientes: Cliente[] = []

  constructor(private clienteService:ClienteService) { }

  ngOnInit(): void {
    this.getClientes()
  }

  getClientes(){
    this.clienteService.getAll().subscribe(
      data => {this.clientes = data}
    )
  }
  
}
