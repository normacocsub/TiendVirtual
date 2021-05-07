import { Usuario } from "./usuario";

export class Interesado {
    nit: string;
    usuario: Usuario;

    constructor(){
        this.nit = '';
        this.usuario = new Usuario();
    }
}
