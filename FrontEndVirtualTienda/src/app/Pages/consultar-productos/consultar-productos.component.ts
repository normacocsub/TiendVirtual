import { Component, OnInit } from '@angular/core';
import { Producto } from 'src/app/models/producto';
import { ProductosService } from 'src/app/services/productos.service';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { VerProductoComponent } from '../ver-producto/ver-producto.component';
import { LoginService } from 'src/app/services/login.service';
import { Usuario } from 'src/app/models/usuario';

@Component({
  selector: 'app-consultar-productos',
  templateUrl: './consultar-productos.component.html',
  styleUrls: ['./consultar-productos.component.css'],
})
export class ConsultarProductosComponent implements OnInit {
  products: Producto[] = [];
  usuario: Usuario = new Usuario();
  constructor(
    private productoService: ProductosService,
    private dialogService: DialogService,
    private loginService: LoginService
  ) {
    this.usuario = this.loginService.currentUserValue;
  }

  ngOnInit(): void {
    this.consultarProductos();
  }

  consultarProductos() {
    this.productoService.consultarProductos().subscribe((result) => {
      this.products = result;
    });
  }

  ref: DynamicDialogRef | undefined;

  show(product: Producto) {
    this.ref = this.dialogService.open(VerProductoComponent, {
      header: 'Informacion Producto',
      width: '40%',
      contentStyle: { 'max-height': '500px', overflow: 'auto' },
      baseZIndex: 10000,
      data: product,
    });

    this.ref.onClose.subscribe((product: Producto) => {
      if (product) {
        //this.messageService.add({severity:'info', summary: 'Product Selected', detail: product.name});
      }
    });
  }
}
