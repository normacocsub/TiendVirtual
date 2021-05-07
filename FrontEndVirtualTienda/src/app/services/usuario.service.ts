import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Usuario } from '../models/usuario';
import { Interesado } from '../models/interesado';

const ruta: string = environment.ruta;
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
};
@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  ruta: string = '';
  constructor(private http: HttpClient) {
    this.ruta = ruta;
  }

  consultarUsuarios(){
    return this.http.get<Usuario[]>(this.ruta + 'api/Usuario');
  }

  registrarUsuario(usuario: Usuario){
    return this.http.post<Usuario>(this.ruta + 'api/Usuario', usuario);
  }

  registrarUsuarioInteresado(interesado: Interesado){
    return this.http.post<Usuario>(this.ruta + "api/Usuario/Interesado", interesado);
  }

  buscarUsuario(email: string){
    return this.http.get<Usuario>(this.ruta + "api/Usuario/"+email);
  }

  editarInfoUsuario(usuario: Usuario){
    return this.http.put<Usuario>(this.ruta + "api/Usuario/ActualizarInfo", usuario, httpOptions);
  }

  actulizarEstadoUsuario(email: string, estado: string){
    return this.http.put<Usuario>(this.ruta + `api/Usuario/UsuarioEstado/${email}/${estado}`, httpOptions);
  }

  consultarInteresados(){
    return this.http.get<Interesado[]>(this.ruta + "api/Usuario/Interesados");
  }
}
