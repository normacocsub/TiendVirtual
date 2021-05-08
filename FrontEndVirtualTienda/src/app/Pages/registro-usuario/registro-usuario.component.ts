import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  AbstractControl,
} from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';

import { Interesado } from 'src/app/models/interesado';
import { Usuario } from 'src/app/models/usuario';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
  selector: 'app-registro-usuario',
  templateUrl: './registro-usuario.component.html',
  styleUrls: ['./registro-usuario.component.css'],
})
export class RegistroUsuarioComponent implements OnInit {
  sexoUsuario = ['Seleccionar Sexo', 'Masculino', 'Femenino'];
  usuario: Usuario = new Usuario();
  interesado: Interesado = new Interesado();
  formGroupUsuario: FormGroup;
  formGroupInteresado: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    private messageService: MessageService,
    private usuarioService: UsuarioService,
    private router: Router,
  ) {
    this.formGroupUsuario = this.formBuilder.group({});
    this.formGroupInteresado = this.formBuilder.group({});
  }

  ngOnInit(): void {
    this.buildFormUsuario();
    this.buildFormInteresado();
  }

  buildFormUsuario() {
    this.usuario = new Usuario();
    this.usuario.email = '';
    this.usuario.password = '';
    this.usuario.nombres = '';
    this.usuario.apellidos = '';
    this.usuario.sexo = '';
    this.usuario.telefono = '';

    this.formGroupUsuario = this.formBuilder.group({
      email: [this.usuario.email, [Validators.required]],
      password: [this.usuario.password, [Validators.required]],
      nombres: [this.usuario.nombres, [Validators.required]],
      apellidos: [this.usuario.apellidos, [Validators.required]],
      sexo: [this.usuario.sexo, [Validators.required]],
      telefono: [this.usuario.telefono, [Validators.required]],
    });
  }

  buildFormInteresado() {
    this.interesado = new Interesado();
    this.interesado.nit = '';
    this.interesado.usuario = new Usuario();
    this.interesado.usuario.email = '';
    this.interesado.usuario.password = '';
    this.interesado.usuario.nombres = '';
    this.interesado.usuario.apellidos = '';
    this.interesado.usuario.sexo = '';
    this.interesado.usuario.telefono = '';

    this.formGroupInteresado = this.formBuilder.group({
      email: [this.usuario.email, [Validators.required]],
      password: [this.usuario.password, [Validators.required]],
      nombres: [this.usuario.nombres, [Validators.required]],
      apellidos: [this.usuario.apellidos, [Validators.required]],
      sexo: [this.usuario.sexo, [Validators.required]],
      telefono: [this.usuario.telefono, [Validators.required]],
      nit: [this.interesado.nit, [Validators.required]],
    });
  }

  get controlUsuario() {
    return this.formGroupUsuario?.controls;
  }

  get controlInteresado() {
    return this.formGroupInteresado?.controls;
  }

  onSubmitUsuario() {
    if (this.formGroupUsuario?.invalid) {
      return;
    }
    this.guardarUsuario();
  }

  onSubmitInteresado() {
    if (this.formGroupInteresado?.invalid) {
      return;
    }
    this.guardarInteresado();
  }

  guardarUsuario() {
    this.usuario = this.formGroupUsuario?.value;

    this.usuarioService.registrarUsuario(this.usuario).subscribe((result) => {
      if (result != null) {
        this.crearMensajeSucessToast('Usuario Registrado');
        this.router.navigate(['/consultarUsuarios']);
      }
    });
  }

  guardarInteresado() {
    this.usuario = this.formGroupInteresado?.value;
    this.interesado.nit = this.formGroupInteresado?.value.nit;
    this.interesado.usuario = this.usuario;
    this.usuarioService
      .registrarUsuarioInteresado(this.interesado)
      .subscribe((result) => {
        if (result != null) {
          this.crearMensajeSucessToast('Usuario interesado Registrado');
          this.router.navigate(['/consultarUsuarios']);
        }
      });
  }

  crearMensajeSucessToast(mensaje: string) {
    this.messageService.add({ severity: 'success', summary: mensaje });
  }
}
