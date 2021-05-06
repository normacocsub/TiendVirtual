import { Component, OnInit } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  visibleSidebar1: boolean = false;
  constructor(private config: PrimeNGConfig){}

  ngOnInit(){
    this.config.setTranslation({
      accept: 'Accept',
      reject: 'Cancel'
    });
    this.config.ripple = true;
  }

  menu(){}
}
