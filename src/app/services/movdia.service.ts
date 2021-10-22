import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MovDia } from '../entities/movdia';

@Injectable({
  providedIn: 'root'
})
export class MovdiaService {

  private url="https://localhost:44332/api/movdia"
  constructor(private http:HttpClient) { }

  listar():Observable<MovDia[]>{
    return this.http.get<MovDia[]>(this.url)
  }
  incluir(body:MovDia):Observable<string>{
    return this.http.post<string>(this.url, body)
  }
}
