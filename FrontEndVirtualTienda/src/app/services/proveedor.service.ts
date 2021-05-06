import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Proveedor } from '../models/proveedor';

const ruta: string = environment.ruta;

@Injectable({
  providedIn: 'root'
})
export class ProveedorService {
  ruta: string = '';
  constructor(private http: HttpClient) { 
    this.ruta = ruta;
  }

  consultarProveedores(){
    return this.http.get<Proveedor[]>(this.ruta+"api/Proveedor");
  }
}
