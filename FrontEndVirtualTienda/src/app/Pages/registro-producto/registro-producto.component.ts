import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  AbstractControl,
} from '@angular/forms';

import { MessageService } from 'primeng/api';
import { Producto } from 'src/app/models/producto';
import { Proveedor } from 'src/app/models/proveedor';
import { ProductosService } from 'src/app/services/productos.service';
import { ProveedorService } from 'src/app/services/proveedor.service';

@Component({
  selector: 'app-registro-producto',
  templateUrl: './registro-producto.component.html',
  styleUrls: ['./registro-producto.component.css'],
})
export class RegistroProductoComponent implements OnInit {
  val2: number = 0;
  selectedCity: string = '';
  proveedorRegistrado: boolean = false;
  proveedores: Proveedor[] = [];
  producto: Producto = new Producto();
  proveedor: Proveedor = new Proveedor();
  selectedProveedor: Proveedor = new Proveedor();
  formGroup: FormGroup;
  formGroupProveedor: FormGroup;
  constructor(
    private messageService: MessageService,
    private formBuilder: FormBuilder,
    private proveedorService: ProveedorService,
    private productoService: ProductosService,
  ) {
    this.formGroup = this.formBuilder.group({});
    this.formGroupProveedor = this.formBuilder.group({});
  }

  ngOnInit(): void {
    this.buildForm();
    this.formBuildProveedor();
    this.consultarProveedores();
  }

  consultarProveedores() {
    this.proveedorService.consultarProveedores().subscribe((result) => {
      this.proveedores = result;
    });
  }

  private buildForm() {
    this.producto = new Producto();
    this.producto.cantidad = 0;
    this.producto.descripcion = '';
    this.producto.descuento = 0;
    this.producto.fecha = new Date();
    this.producto.idProveedor = '';
    this.producto.iva = 0;
    this.producto.proveedor = new Proveedor();
    this.producto.valorDescontado = 0;
    this.producto.valorTotal = 0;
    this.producto.valorUnitario = 0;

    this.formGroup = this.formBuilder.group({
      cantidad: [this.producto.cantidad, [Validators.required]],
      descripcion: [this.producto.descripcion, [Validators.required]],
      descuento: [this.producto.descuento, [Validators.required]],
      fecha: [this.producto.fecha, [Validators.required]],
      iva: [this.producto.iva, [Validators.required]],
      valorDescontado: [this.producto.valorDescontado, [Validators.required]],
      valorTotal: [this.producto.valorTotal, [Validators.required]],
      valorUnitario: [this.producto.valorUnitario, [Validators.required]],
    });
  }

  private formBuildProveedor() {
    this.proveedor = new Proveedor();
    this.proveedor.nit = '';
    this.proveedor.nombre = '';
    this.proveedor.telefono = '';

    this.formGroupProveedor = this.formBuilder.group({
      nit: [this.proveedor.nit, [Validators.required]],
      nombre: [this.proveedor.nombre, [Validators.required]],
      telefono: [this.proveedor.telefono, [Validators.required]],
    });
  }

  get control() {
    return this.formGroup?.controls;
  }

  get controlProveedor() {
    return this.formGroup?.controls;
  }

  //verificacion para no guardar si los formularios estan invalidos
  onSubmit() {
    if (this.proveedorRegistrado) {
      if (this.formGroupProveedor?.invalid) {
        return;
      }
    } else {
      if (this.selectedProveedor.nit == '') {
        return;
      }
    }
    if (this.formGroup?.invalid) {
      return;
    }
    this.guardarProducto();
  }

  onSubmitProveedor() {
    if (this.formGroupProveedor?.invalid) {
      return;
    }
    this.saveProveedor();
  }

  saveProveedor() {
    if (!this.proveedorRegistrado) {
      this.proveedor = this.formGroupProveedor?.value;
      this.crearMensajeSucessToast('Proveedor Guardado');
    } else {
      this.crearMensajeInfoToast('Ya esta registrado el proveedor');
    }
    this.proveedorRegistrado = true;
  }

  guardarProducto() {
    this.producto = this.formGroup?.value;
    if (this.proveedorRegistrado) {
      this.producto.proveedor = this.proveedor;
    } else {
      this.producto.proveedor = this.selectedProveedor;
    }
    this.productoService.guardarProducto(this.producto).subscribe(result => {
      this.crearMensajeSucessToast("Producto Registrado");
    });
    console.log(this.producto);
  }

  //componente de slide para seleccionar el descuento
  handleChange(event: any) {
    this.producto.descuento = event.value;
  }

  borrarProveedor() {
    this.proveedorRegistrado = false;
  }

  crearMensajeSucessToast(mensaje: string) {
    this.messageService.add({ severity: 'success', summary: mensaje });
  }

  crearMensajeInfoToast(mensaje: string) {
    this.messageService.add({ severity: 'info', summary: mensaje });
  }

  onRowSelect(event: any) {
    this.crearMensajeInfoToast("Proveedor Seleccionado");
  }
}
