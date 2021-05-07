import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Usuario } from '../models/usuario';
import {  map, tap } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';

const ruta: string = environment.ruta;
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
};

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private currentUserSubject: BehaviorSubject<Usuario>;
  public currentUser: Observable<Usuario>;
  ruta: string = '';
  constructor(private http: HttpClient) {
    this.ruta = ruta;
    this.currentUserSubject = new BehaviorSubject<Usuario>(JSON.parse(sessionStorage.getItem('LoginTienda') || '{}'));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  login(user: string, password: string){
    return this.http.get<Usuario>(this.ruta + `api/Usuario/login/${user}/${password}`).pipe(
      map((user:Usuario) => {
        sessionStorage.setItem('LoginTienda', JSON.stringify(user));
        return user;
      })
    )
  }


  public get currentUserValue(): Usuario {
    return this.currentUserSubject.value;
  }

  logout() {
    sessionStorage.removeItem('LoginTienda');
    return;
  }

}
