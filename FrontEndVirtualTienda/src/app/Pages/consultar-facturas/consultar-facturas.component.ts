import { Component, OnInit } from '@angular/core';
import { Factura } from 'src/app/models/factura';
import { FacturaService } from 'src/app/services/factura.service';

@Component({
  selector: 'app-consultar-facturas',
  templateUrl: './consultar-facturas.component.html',
  styleUrls: ['./consultar-facturas.component.css']
})
export class ConsultarFacturasComponent implements OnInit {
  facturas: Factura[] = [];
  constructor(private facturaService: FacturaService) { }

  ngOnInit(): void {
    this.consultarFacturas();
  }

  consultarFacturas(){
    this.facturaService.consultarFacturas().subscribe(result => {
      this.facturas = result;
      console.log(result);
    })
  }

}
