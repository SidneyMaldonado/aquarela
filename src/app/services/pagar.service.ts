import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pagar } from '../entities/pagar';
import { PagarLista } from '../entities/pagarLista';


@Injectable({
  providedIn: 'root'
})
export class PagarService {

  constructor(private http: HttpClient) { }

  listar() : Observable<Pagar[]> {
    return this.http.get<Pagar[]>("https://localhost:44332/api/pagar")
  }
  incluir(novoPagar: Pagar): Observable<Pagar[]> {
    return this.http.post<Pagar[]>("https://localhost:44332/api/pagar", novoPagar)
  }
}
