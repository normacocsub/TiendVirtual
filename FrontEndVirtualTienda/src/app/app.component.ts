import { Component, OnInit } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { MenuItem } from 'primeng/api';
import { Usuario } from './models/usuario';
import { LoginService } from './services/login.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  visibleSidebar1: boolean = false;
  items: MenuItem[] = [];
  logged: boolean = false;
  usuario: Usuario | undefined;
  constructor(
    private config: PrimeNGConfig,
    private loginService: LoginService
  ) {
    this.loginService.currentUser.subscribe(result => {
      if(result.email){
        this.logged = true;
        console.log(result);
        if(result.role == "Ventas"){
          this.menuUsuarioVentas();
        }
        if(result.role == "Interesado"){
          this.menuUsuarioInteresado();
        }
        if(result.role == "LIDER"){
          this.menuAdmin();
        }
      }
      else{
        this.logged = false;
      }
    });
  }

  ngOnInit() {
    this.config.setTranslation({
      accept: 'Accept',
      reject: 'Cancel',
    });
    this.config.ripple = true;
  }

  menu() {
    this.items = [
      {
        label: 'Productos',
        icon: 'pi pi-briefcase',
        items: [
          {
            label: 'Registrar',
            icon: 'pi pi-plus',
            routerLink: ['/registroProducto'],
          },
          {
            label: 'Consultar',
            icon: 'pi pi-search',
            routerLink: ['/consultarProductos'],
          },
        ],
      },
      {
        label: 'Usuarios',
        icon: 'pi pi-user',
        items: [
          {
            label: 'Registrar',
            icon: 'pi pi-plus',
            routerLink: ['/registroUsuario'],
          },
          {
            label: 'Consultar',
            icon: 'pi pi-search',
            routerLink: ['/consultarUsuarios'],
          },
        ],
      },
      {
        label: 'Facturas',
        icon: 'pi pi-wallet',
        items: [
          {
            label: 'Facturar',
            icon: 'pi pi-plus',
            routerLink: ['/crearFactura'],
          },
          {
            label: 'Consultar',
            icon: 'pi pi-search',
            routerLink: ['/consultarFacturas'],
          },
        ],
      },
    ];
  }

  menuAdmin() {
    this.items = [
      {
        label: 'Home',
        icon: 'pi pi-home',
        routerLink: ['/'],
      },
      {
        label: 'Productos',
        icon: 'pi pi-briefcase',
        items: [
          {
            label: 'Registrar',
            icon: 'pi pi-plus',
            routerLink: ['/registroProducto'],
          },
          {
            label: 'Consultar',
            icon: 'pi pi-search',
            routerLink: ['/consultarProductos'],
          },
        ],
      },
      {
        label: 'Usuarios',
        icon: 'pi pi-user',
        items: [
          {
            label: 'Registrar',
            icon: 'pi pi-plus',
            routerLink: ['/registroUsuario'],
          },
          {
            label: 'Consultar',
            icon: 'pi pi-search',
            routerLink: ['/consultarUsuarios'],
          },
        ],
      },
      {
        separator: true
      },
      {
        label: 'Cerrar Sesion',
        icon: 'pi pi-sign-out',
        command: () => {
          this.logout();
        }
      }
    ];
  }

  menuUsuarioInteresado() {
    this.items = [
      {
        label: 'Home',
        icon: 'pi pi-home',
        routerLink: ['/'],
      },
      {
        label: 'Productos',
        icon: 'pi pi-briefcase',
        items: [
          {
            label: 'Consultar',
            icon: 'pi pi-search',
            routerLink: ['/consultarProductos'],
          },
        ],
      },
      {
        separator: true
      },
      {
        label: 'Cerrar Sesion',
        command: () => {
          this.logout();
        }
      }
    ];
  }

  menuUsuarioVentas() {
    this.items = [
      {
        label: 'Home',
        icon: 'pi pi-home',
        routerLink: ['/'],
      },
      {
        label: 'Facturas',
        icon: 'pi pi-wallet',
        items: [
          {
            label: 'Facturar',
            icon: 'pi pi-plus',
            routerLink: ['/crearFactura'],
          },
          {
            label: 'Consultar',
            icon: 'pi pi-search',
            routerLink: ['/consultarFacturas'],
          },
        ],
      },
      {
        label: 'Productos',
        icon: 'pi pi-briefcase',
        items: [
          {
            label: 'Consultar',
            icon: 'pi pi-search',
            routerLink: ['/consultarProductos'],
          },
        ],
      },
      {
        separator: true
      },
      {
        label: 'Cerrar Sesion',
        command: () => {
          this.logout();
        }
      }
    ];
  }

  logout(){
    this.loginService.logout();
    window.location.reload();
  }
}
