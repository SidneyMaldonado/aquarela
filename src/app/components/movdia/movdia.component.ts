import { Component, OnInit } from '@angular/core';
import { MovDia } from 'src/app/entities/movdia';
import { MovdiaService } from 'src/app/services/movdia.service';

@Component({
  selector: 'app-movdia',
  templateUrl: './movdia.component.html',
  styleUrls: ['./movdia.component.css']
})
export class MovdiaComponent implements OnInit {
  movdias:MovDia[]=[]
  constructor(private movdiaService:MovdiaService) { }

  ngOnInit(): void {
    this.movdiaService.listar().subscribe(
      data=>this.movdias=data
    )
  }

}
