import { Detalle } from "./detalle";

export class Factura {

    codigo: number | undefined;
    iva: number;
    descuento: number;
    cantidad: number;
    estado: string
    estadoTransaccion: string;
    total: number;
    fecha: Date;
    interesadoId: string | undefined;
    usuarioVentasId: string | undefined;
    detalles: Detalle[];


    constructor(){
      this.codigo = 0;
      this.cantidad = 0;
      this.estado = '';
      this.estadoTransaccion = '';
      this.interesadoId = '';
      this.usuarioVentasId = '';
      this.total = 0;
      this.fecha = new Date();
      this.detalles = [];
      this.descuento = 0;
      this.iva = 0;
    }

    calcularTotal(){
      if(this.detalles.length > 0){
        this.detalles.forEach(d => {
          this.total += (d.valorTotal * d.cantidad);
          this.descuento += (d.valorDescuento * d.cantidad);
          this.cantidad += (d.cantidad);
          if(d.descuento < 100){
            this.iva += (d.iva * d.cantidad);
          }
        });
      }
    }
}
