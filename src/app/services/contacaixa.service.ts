import { ContaCaixaLista } from './../entities/contacaixaLista';
import { ContaCaixa } from './../entities/contacaixa';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ContaCaixaService {

  private url = "https://localhost:44332/api/contacaixa"

  constructor( private http: HttpClient) { }


  listar() : Observable<ContaCaixaLista[]> {

    return this.http.get<ContaCaixaLista[]>(this.url);

  }
  incluir(body:ContaCaixa) : Observable<string>{
    return this.http.post<string>(this.url, body)
  }

}
