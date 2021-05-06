import { Component, OnInit } from '@angular/core';
import { ProductosService } from 'src/app/services/productos.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private productoService: ProductosService) { }

  ngOnInit(): void {
    this.productoService.consultarProductos().subscribe(result => {
      console.log(result);
    })
  }

}
