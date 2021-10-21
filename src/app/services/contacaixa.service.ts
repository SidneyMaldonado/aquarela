import { ContaCaixa } from './../entities/contacaixa';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ContaCaixaService {

  constructor( private http: HttpClient) { }


  listar() : Observable<ContaCaixa[]> {

    return this.http.get<ContaCaixa[]>("https://localhost:44332/api/contacaixa");

  }

}
