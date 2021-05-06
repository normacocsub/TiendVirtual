import { Proveedor } from "./proveedor";

export class Producto {
    descripcion: string;
    cantidad: number;
    fecha: Date;
    descuento: number;
    iva: number;
    valorDescontado: number;
    valorTotal: number;
    proveedor: Proveedor;
    idProveedor: string;
    valorUnitario: number;

    constructor(){
        this.descripcion = '';
        this.cantidad = 0;
        this.fecha = new Date();
        this.descuento = 0;
        this.iva = 0;
        this.valorDescontado = 0;
        this.valorTotal = 0;
        this.proveedor = new Proveedor();
        this.idProveedor = '';
        this.valorUnitario = 0;
    }
}
