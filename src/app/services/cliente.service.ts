import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cliente } from '../entities/cliente';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  url = "https://localhost:44332/api/cliente"

  constructor(private http: HttpClient) {}

  getAll(): Observable<Cliente[]>{
    return this.http.get<Cliente[]>(`${this.url}`)
  }
}
