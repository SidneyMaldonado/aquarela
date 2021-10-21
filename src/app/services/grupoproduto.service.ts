import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GrupoProduto } from '../entities/grupoproduto';

@Injectable({
  providedIn: 'root'
})
export class GrupoProdutoService {

  private apiUrl: string = 'https://localhost:44332/api/grupoproduto'

  constructor(private http: HttpClient) { }

  listar(): Observable<GrupoProduto[]> {
    return this.http.get<GrupoProduto[]>(`${this.apiUrl}`)
  }

}
