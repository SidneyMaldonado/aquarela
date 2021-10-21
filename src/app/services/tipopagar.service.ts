import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TipoPagar } from '../entities/tipoPagar';

@Injectable({
  providedIn: 'root'
})
export class TipopagarService {

  constructor(private http: HttpClient) { }
  listar() : Observable<TipoPagar[]> {

    return this.http.get<TipoPagar[]>("https://localhost:44332/api/tipopagar");

  }
}
