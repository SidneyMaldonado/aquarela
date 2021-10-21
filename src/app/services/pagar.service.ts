import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PagarLista } from '../entities/pagarLista';


@Injectable({
  providedIn: 'root'
})
export class PagarService {

  constructor(private http: HttpClient) { }

  listar() : Observable<PagarLista[]> {

    return this.http.get<PagarLista[]>("https://localhost:44332/api/pagar")
  }
}
