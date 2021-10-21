import { Component, OnInit } from '@angular/core';
import { VendaItem } from 'src/app/entities/vendaItem';
import { VendaitemService } from 'src/app/services/vendaitem.service';

@Component({
  selector: 'app-vendaitem',
  templateUrl: './vendaitem.component.html',
  styleUrls: ['./vendaitem.component.css']
})
export class VendaitemComponent implements OnInit {

  vendaItens: VendaItem[] = []

  constructor(private serviceVendaItem: VendaitemService) { }

  ngOnInit(): void {
    this.getVendaItem()
  }

  getVendaItem(){
    this.serviceVendaItem.getAll().subscribe(
      data => this.vendaItens = data
    )
  }

}
