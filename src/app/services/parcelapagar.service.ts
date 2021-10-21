import { ParcelaPagar } from './../entities/parcelapagar';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ParcelaPagarService {

  constructor( private http: HttpClient) { }


  listar() : Observable<ParcelaPagar[]> {

    return this.http.get<ParcelaPagar[]>("https://localhost:44332/api/parcelapagar");

  }

}