import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { VendaItem } from '../entities/vendaItem';

@Injectable({
  providedIn: 'root'
})
export class VendaitemService {

  url = "https://localhost:44332/api/vendaitem"

  constructor(private http: HttpClient) { }

  getAll(): Observable<VendaItem[]>{
    return this.http.get<VendaItem[]>(`${this.url}`)
  }
  
}
