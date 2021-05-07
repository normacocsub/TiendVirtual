import { Proveedor } from "./proveedor";

export class Producto {
    codigo: string | undefined;
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
    cantidadSeleccionada: number = 0;

    constructor(){
        this.codigo = undefined;
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
