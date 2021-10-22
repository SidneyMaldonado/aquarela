import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Venda } from '../entities/venda';

@Injectable({
  providedIn: 'root'
})
export class VendaService {

  private url = "https://localhost:44332/api/venda"

  constructor(private http: HttpClient) { }

  listar(): Observable<Venda[]>{
    return this.http.get<Venda[]>(`${this.url}`)
  }

}
