import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GuardGuard implements CanActivate {
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    console.log(route.data);
    return this.verificarUsuario(route);
  }


  verificarUsuario(data: ActivatedRouteSnapshot){
    var user = JSON.parse(sessionStorage.getItem('LoginTienda') || '{}');
    if(user.email){
      var estado = false;
      if(data.data.role == user.role){
        estado = true;
      }
      if(data.data.role2 == user.role){
        estado = true;
      }

      if(data.data.role3 == user.role){
        estado = true;
      }

      if(!estado){
        return false;
      }
      return true;


    }
    return true;;
  }

}
