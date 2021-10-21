import { ContaCaixaLista } from './../entities/contacaixaLista';
import { ContaCaixa } from './../entities/contacaixa';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ContaCaixaService {

  constructor( private http: HttpClient) { }


  listar() : Observable<ContaCaixaLista[]> {

    return this.http.get<ContaCaixaLista[]>("https://localhost:44332/api/contacaixa");

  }

}
