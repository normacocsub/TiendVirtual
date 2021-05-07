import { Producto } from "./producto";

export class Detalle {

  fecha: Date;
  descuento: number;
  iva: number;
  valorTotal: number;
  cantidad: number;
  valorDescuento: number;
  valorUnitario: number;
  producto: Producto;

  constructor(){
    this.producto = new Producto();
    this.fecha = new Date();
    this.descuento = 0;
    this.iva = 0;
    this.valorTotal = 0;
    this.cantidad = 0;
    this.valorDescuento = 0;
    this.valorUnitario = 0;
  }

        calcularDescuento()
        {
            this.valorDescuento = ( this.valorUnitario * ( this.descuento / 100 ));
        }

        calcularIVA()
        {
            this.iva = ( this.valorUnitario * 0.19 );
        }

        calcularTotal()
        {
            this.calcularDescuento();
            this.calcularIVA();
            if(this.descuento == 100)
            {
                return this.valorTotal = 0;
            }
            return this.valorTotal = ( this.iva + (this.valorUnitario - this.valorDescuento ));
        }


}
