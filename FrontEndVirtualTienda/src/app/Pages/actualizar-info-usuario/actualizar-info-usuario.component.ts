import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Usuario } from 'src/app/models/usuario';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
  selector: 'app-actualizar-info-usuario',
  templateUrl: './actualizar-info-usuario.component.html',
  styleUrls: ['./actualizar-info-usuario.component.css']
})
export class ActualizarInfoUsuarioComponent implements OnInit {
  sexoUsuario = ['Seleccionar Sexo','Masculino', 'Femenino'];
  usuario: Usuario = new Usuario();
  constructor(private routeActive: ActivatedRoute, private usuarioService: UsuarioService, private messageService: MessageService) { }

  ngOnInit(): void {
    const email = this.routeActive.snapshot.params.email;
    this.buscarUsuario(email);

  }

  buscarUsuario(email: string){
    this.usuarioService.buscarUsuario(email).subscribe(result => {
      this.usuario = result;
      console.log(this.usuario);
    });
  }

  actualizar(){
    var result = this.validarCampos();
    if(result){
      this.crearMensajeInfoToast("No pueden haber campos vacios");
      return;
    }
    this.usuarioService.editarInfoUsuario(this.usuario).subscribe(result => {
      this.crearMensajeSucessToast("Se ha actualizado el usuario")
      this.usuario = result;
    });
  }

  crearMensajeSucessToast(mensaje: string) {
    this.messageService.add({ severity: 'success', summary: mensaje });
  }

  crearMensajeInfoToast(mensaje: string) {
    this.messageService.add({ severity: 'info', summary: mensaje });
  }

  validarCampos(){
    if(this.usuario.apellidos.trim() == ''){
      return true;
    }
    if(this.usuario.nombres.trim() == ''){
      return true;
    }
    if(this.usuario.telefono.trim() == ''){
      return true;
    }
    return false;
  }

}
