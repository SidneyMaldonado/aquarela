import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Receber } from '../entities/receber';
import { ReceberLista } from '../entities/receberLista';

@Injectable({
  providedIn: 'root'
})
export class ReceberService {
  private url="https://localhost:44332/api/receber"
  constructor(private http:HttpClient) { }

  listar():Observable<ReceberLista[]>{
    return this.http.get<ReceberLista[]>(this.url)
  }
}
