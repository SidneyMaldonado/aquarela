import { ParcelaReceber } from './../entities/parcelareceber';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ParcelaReceberService {

  constructor( private http: HttpClient) { }


  listar() : Observable<ParcelaReceber[]> {

    return this.http.get<ParcelaReceber[]>("https://localhost:44332/api/parcelareceber");

  }

}
