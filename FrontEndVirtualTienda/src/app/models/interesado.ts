import { Usuario } from "./usuario";

export class Interesado {
    nit: string | undefined;
    usuario: Usuario;

    constructor(){
        this.nit = '';
        this.usuario = new Usuario();
    }
}
