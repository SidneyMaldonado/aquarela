import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Produto } from '../entities/produto';

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {

  private apiUrl: string = 'https://localhost:44332/api/produto'

  constructor(private http: HttpClient) { }

  listar(): Observable<Produto[]> {
    return this.http.get<Produto[]>(`${this.apiUrl}`)
  }

}
