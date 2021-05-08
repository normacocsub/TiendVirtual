import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig } from 'primeng/dynamicdialog';
import { Detalle } from 'src/app/models/detalle';
import { Factura } from 'src/app/models/factura';

@Component({
  selector: 'app-ver-factura',
  templateUrl: './ver-factura.component.html',
  styleUrls: ['./ver-factura.component.css']
})
export class VerFacturaComponent implements OnInit {
  detalles: Detalle[] = [];
  factura: Factura = new Factura();
  constructor(public config: DynamicDialogConfig) { }

  ngOnInit(): void {
    this.factura = this.config.data;
    this.detalles = this.config.data.detalles;
    console.log(this.detalles);
  }

}
