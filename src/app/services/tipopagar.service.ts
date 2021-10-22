import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TipoPagar } from '../entities/tipoPagar';

@Injectable({
  providedIn: 'root'
})
export class TipopagarService {
  private url = "https://localhost:44332/api/tipopagar"
  constructor(private http: HttpClient) { }
  listar() : Observable<TipoPagar[]> {

    return this.http.get<TipoPagar[]>(this.url);

  }

  incluir(body:TipoPagar):Observable<string>{
    return this.http.post<string>(this.url, body)
  }
}
