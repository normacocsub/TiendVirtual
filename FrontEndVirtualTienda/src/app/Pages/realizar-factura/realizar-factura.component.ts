import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Detalle } from 'src/app/models/detalle';
import { Factura } from 'src/app/models/factura';
import { Interesado } from 'src/app/models/interesado';
import { Producto } from 'src/app/models/producto';
import { Usuario } from 'src/app/models/usuario';
import { FacturaService } from 'src/app/services/factura.service';
import { LoginService } from 'src/app/services/login.service';
import { ProductosService } from 'src/app/services/productos.service';
import { UsuarioService } from 'src/app/services/usuario.service';
import { DetallesProductoCompraComponent } from '../detalles-producto-compra/detalles-producto-compra.component';

@Component({
  selector: 'app-realizar-factura',
  templateUrl: './realizar-factura.component.html',
  styleUrls: ['./realizar-factura.component.css'],
})
export class RealizarFacturaComponent implements OnInit {
  sourceProducts: Producto[] = [];

  targetProducts: Producto[] = [];
  cantidad: number = 0;
  valor: number = 0;
  descuento: number = 0;
  producto: Producto = new Producto();
  detalles: Detalle[] = [];
  detalle: Detalle = new Detalle();
  factura: Factura = new Factura();
  interesado: Interesado = new Interesado();
  interesados: Interesado[] = [];
  estadoClienteChecked: boolean = true;
  usuario: Usuario = new Usuario();
  constructor(
    private productoService: ProductosService,
    private usuarioService: UsuarioService,
    private facturaService: FacturaService,
    private router: Router,
    private loginService: LoginService,
    private messageService: MessageService,
  ) {
    this.usuario = this.loginService.currentUserValue;
  }

  ngOnInit(): void {
    this.consultarProductos();
    this.consultarInteresados();
  }

  consultarProductos() {
    this.productoService.consultarProductos().subscribe((result) => {
      this.sourceProducts = result;
      this.sourceProducts.forEach((d) => (d.cantidadSeleccionada = 0));
      this.targetProducts = [];
    });
  }

  consultarInteresados() {
    this.usuarioService.consultarInteresados().subscribe((result) => {
      this.interesados = result;
    });
  }

  eventoChangeSwitch(event: any) {
    if (!event.checked) {
      this.interesado = new Interesado();
    }
  }

  evento(event: any) {
    this.producto = event;
    var index = this.targetProducts.findIndex(
      (d) => d.codigo == this.producto.codigo
    );
    if (this.producto.cantidad < this.cantidad || this.cantidad == 0) {
      this.targetProducts.splice(index, 1);
      this.sourceProducts.push(this.producto);

      return;
    }
    if (this.valor > 0) {
      this.producto.valorUnitario = this.valor;
    }
    this.producto.cantidadSeleccionada = this.cantidad;

    this.targetProducts[index] = this.producto;
  }

  eventoRegreso(event: any) {
    this.producto = event;
    var index = this.sourceProducts.findIndex(
      (d) => d.codigo == this.producto.codigo
    );
    this.producto.cantidadSeleccionada = 0;
    this.sourceProducts[index] = this.producto;
  }

  registrarDetalles() {
    this.factura = new Factura();
    if (this.targetProducts.length > 0) {
      this.detalles = [];
      this.factura.detalles = [];
      this.targetProducts.forEach((p) => {
        this.detalle = new Detalle();
        this.detalle.producto = p;
        this.detalle.cantidad = p.cantidadSeleccionada;
        this.detalle.valorUnitario = p.valorUnitario;
        this.detalle.descuento = p.descuento;
        this.detalle.calcularTotal();
        this.detalles.push(this.detalle);
      });

      this.factura.detalles = this.detalles;
      this.factura.calcularTotal();
    }
  }

  guardarFactura() {
    this.factura.interesadoId = this.interesado.nit;
    //se asginan undefined las claves para que en el back se autoasignen
    if (!this.estadoClienteChecked) {
      this.interesado.nit = undefined;
      this.factura.interesadoId = undefined;
    }
    if (this.detalles.length == 0) {
      this.crearMensajeInfoToast("No se han seleccionado Productos aun");
      return;
    }

    this.factura.usuarioVentasId = this.usuario.email;
    this.factura.codigo = undefined;

    this.facturaService.guardarFactura(this.factura).subscribe(result => {
      this.crearMensajeSucessToast("Factura Registrada Correctamente")
      this.router.navigate(['/consultarFacturas']);
      console.log(result);
    })
  }

  onRowSelect(event: any) {}

  crearMensajeSucessToast(mensaje: string) {
    this.messageService.add({ severity: 'success', summary: mensaje });
  }

  crearMensajeInfoToast(mensaje: string) {
    this.messageService.add({ severity: 'info', summary: mensaje });
  }
}
