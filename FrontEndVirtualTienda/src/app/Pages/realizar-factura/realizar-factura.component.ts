import { Component, OnInit } from '@angular/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Detalle } from 'src/app/models/detalle';
import { Producto } from 'src/app/models/producto';
import { ProductosService } from 'src/app/services/productos.service';
import { DetallesProductoCompraComponent } from '../detalles-producto-compra/detalles-producto-compra.component';

@Component({
  selector: 'app-realizar-factura',
  templateUrl: './realizar-factura.component.html',
  styleUrls: ['./realizar-factura.component.css']
})
export class RealizarFacturaComponent implements OnInit {
  sourceProducts: Producto[] = [];

  targetProducts: Producto[] = [];
  cantidad: number = 0;
  valor: number = 0;
  descuento: number = 0;
  producto : Producto = new Producto();
  detalles: Detalle[] = [];
  detalle: Detalle = new Detalle();
  constructor(private productoService: ProductosService, private dialogService: DialogService) { }

  ngOnInit(): void {
    this.consultarProductos();
  }

  consultarProductos(){
    this.productoService.consultarProductos().subscribe(result => {
      this.sourceProducts = result;
      this.sourceProducts.forEach(d=> d.cantidadSeleccionada = 0);
      this.targetProducts = [];
    });
  }


 evento(event: any){

  this.producto = event.items;
  var index = this.targetProducts.findIndex(d => d.codigo == event.items.codigo);
  if(this.producto.cantidad < this.cantidad || this.cantidad == 0){
    this.targetProducts.splice(index, 1);
    this.sourceProducts.push(this.producto);

    return;
  }
  if(this.valor > 0){
    this.producto.valorUnitario = this.valor;
  }
  this.producto.cantidadSeleccionada = this.cantidad;

  this.targetProducts[index] = this.producto;

  this.cantidad = 0;
  this.valor = 0;
  console.log(this.targetProducts);
  }

  eventoRegreso(event: any){
    this.producto = event.items;
    var index = this.sourceProducts.findIndex(d => d.codigo == event.items.codigo);
    this.producto.cantidadSeleccionada = 0;
    this.sourceProducts[index] = this.producto;
  }

  registrarDetalles(){
    if(this.targetProducts.length>0){
      this.targetProducts.forEach(p => {
        this.detalle = new Detalle();
        this.detalle.producto = p;
        this.detalle.cantidad = p.cantidadSeleccionada;
        this.detalle.valorUnitario = p.valorUnitario;
        this.detalle.descuento = p.descuento;
        this.detalle.calcularTotal();
      })
    }
  }


}
