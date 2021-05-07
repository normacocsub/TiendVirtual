import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Producto } from '../models/producto';

const ruta: string = environment.ruta;

@Injectable({
  providedIn: 'root'
})
export class ProductosService {
  ruta: string = '';
  constructor(private http: HttpClient) {
    this.ruta = ruta;
  }

  consultarProductos(){
    return this.http.get<Producto[]>(this.ruta+"api/Producto");
  }

  guardarProducto(producto: Producto){
    return this.http.post<Producto>(this.ruta+"api/Producto", producto);
  }
}
