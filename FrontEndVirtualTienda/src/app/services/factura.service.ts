import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Factura } from '../models/factura';

const ruta: string = environment.ruta;
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
};

@Injectable({
  providedIn: 'root'
})
export class FacturaService {

  ruta: string = '';
  constructor(private http: HttpClient) {
    this.ruta = ruta;
  }

  guardarFactura(factura: Factura){
    return this.http.post<Factura>(this.ruta + "api/Factura", factura);
  }

  consultarFacturas(){
    return this.http.get<Factura[]>(this.ruta +"api/Factura");
  }
}
