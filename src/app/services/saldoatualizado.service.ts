import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SaldoAtualizadoLista } from '../entities/saldoAtualizadoLista';

@Injectable({
  providedIn: 'root'
})
export class SaldoatualizadoService {

  constructor(private http: HttpClient) { }

  listar() : Observable<SaldoAtualizadoLista[]>{
    return this.http.get<SaldoAtualizadoLista[]>("https://localhost:44332/api/saldoatualizado")
  }

  incluir(novoSaldo: SaldoAtualizadoLista): Observable<SaldoAtualizadoLista> {
    return this.http.post<SaldoAtualizadoLista>("https://localhost:44332/api/saldoatualizado", novoSaldo)
  }
  
}
