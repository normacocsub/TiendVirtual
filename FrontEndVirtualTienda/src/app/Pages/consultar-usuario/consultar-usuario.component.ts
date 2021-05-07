import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Usuario } from 'src/app/models/usuario';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
  selector: 'app-consultar-usuario',
  templateUrl: './consultar-usuario.component.html',
  styleUrls: ['./consultar-usuario.component.css'],
})
export class ConsultarUsuarioComponent implements OnInit {
  first = 0;
  rows = 10;
  usuarios: Usuario[] = [];
  items: any[] = [];
  usuarioSelected: Usuario = new Usuario();
  constructor(private serviceUsuario: UsuarioService, private router: Router, private messageService: MessageService) {}

  ngOnInit(): void {
    this.consultarUsuario();
    this.itemsButton();
  }

  itemsButton(){
    this.items = [
      {label: 'Update', icon: 'pi pi-refresh' ,command: () => {
          this.update(this.usuarioSelected);
      }},
      {label: 'Cambiar Estado', icon: 'pi pi-times', command: () => {
          this.actualizarEstado(this.usuarioSelected);
      }}
    ];
  }

  consultarUsuario(){
    this.serviceUsuario.consultarUsuarios().subscribe(result => {
      this.usuarios = result;
      console.log(this.usuarios);
    })
  }

  actualizarEstado(usuario: Usuario){
    var estado = '';
    if(usuario.estado === "Activo"){
      estado = 'No Activo';
    }
    else{
      estado = 'Activo';
    }
    this.serviceUsuario.actulizarEstadoUsuario(usuario.email, estado).subscribe(result =>{
      if(result != null){
        this.crearMensajeSucessToast("Estado actualizado");
        var index = this.usuarios.findIndex(u => u.email == result.email);
        this.usuarios[index] = result;
      }
    });
  }

  crearMensajeSucessToast(mensaje: string) {
    this.messageService.add({ severity: 'success', summary: mensaje });
  }

  update(usuario: Usuario){
    this.router.navigate([`updateInfoUsuario/${usuario.email}`]);
  }

  datoUsuarioButton(usuario: Usuario){
    this.usuarioSelected = usuario;
  }

  next() {
    this.first = this.first + this.rows;
  }

  prev() {
    this.first = this.first - this.rows;
  }

  reset() {
    this.first = 0;
  }

  isLastPage(): boolean {
    return this.usuarios
      ? this.first === this.usuarios.length - this.rows
      : true;
  }

  isFirstPage(): boolean {
    return this.usuarios ? this.first === 0 : true;
  }
}
