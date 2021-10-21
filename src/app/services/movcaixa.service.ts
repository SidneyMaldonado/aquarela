import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Movcaixa } from '../entities/movcaixa';

@Injectable({
  providedIn: 'root'
})
export class MovcaixaService {

  constructor(private http: HttpClient) { }

  listar() : Observable<Movcaixa[]> {

    return this.http.get<Movcaixa[]>("https://localhost:44332/api/movcaixa");

  }
}
