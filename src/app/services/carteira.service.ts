import { HttpClient } from '@angular/common/http';
import { Carteira } from './../entities/carteira';
import { Injectable } from '@angular/core';
import { CarteiraComponent } from '../components/carteira/carteira.component';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CarteiraService {

  constructor(private http: HttpClient) { }

  listar() : Observable<Carteira[]> {

    return this.http.get<Carteira[]>("https://localhost:44332/api/carteira");

  }
  incluir(fornecedor: Carteira): Observable<Carteira> {
    return this.http.post<Carteira>("https://localhost:44332/api/carteira", fornecedor)
  } 

}
