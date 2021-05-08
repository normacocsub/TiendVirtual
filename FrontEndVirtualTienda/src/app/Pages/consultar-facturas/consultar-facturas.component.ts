import { Component, OnInit } from '@angular/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Factura } from 'src/app/models/factura';
import { FacturaService } from 'src/app/services/factura.service';
import { VerFacturaComponent } from '../ver-factura/ver-factura.component';

@Component({
  selector: 'app-consultar-facturas',
  templateUrl: './consultar-facturas.component.html',
  styleUrls: ['./consultar-facturas.component.css']
})
export class ConsultarFacturasComponent implements OnInit {
  facturas: Factura[] = [];
  constructor(private facturaService: FacturaService,
    private dialogService: DialogService,) { }

  ngOnInit(): void {
    this.consultarFacturas();
  }

  consultarFacturas(){
    this.facturaService.consultarFacturas().subscribe(result => {
      this.facturas = result;
      console.log(result);
    })
  }
  ref: DynamicDialogRef | undefined;
  show(factura: Factura) {
    this.ref = this.dialogService.open(VerFacturaComponent, {
      header: 'Informacion Factura',
      width: '80%',
      contentStyle: { 'max-height': '600px', overflow: 'auto' },
      baseZIndex: 10000,
      data: factura,
    });

    this.ref.onClose.subscribe((factura: Factura) => {
      if (factura) {
        //this.messageService.add({severity:'info', summary: 'Product Selected', detail: product.name});
      }
    });
  }

}
