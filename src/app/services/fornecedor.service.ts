import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Fornecedor } from '../entities/fornecedor';

@Injectable({
  providedIn: 'root'
})
export class FornecedorService {

  private apiUrl: string = 'https://localhost:44332/api/fornecedor'

  constructor(private http: HttpClient) { 
  }

  listar(): Observable<Fornecedor[]> {
    return this.http.get<Fornecedor[]>(`${this.apiUrl}`)
  }

  incluir(fornecedor: Fornecedor): Observable<Fornecedor> {
    return this.http.post<Fornecedor>(this.apiUrl, fornecedor)
  } 

}
