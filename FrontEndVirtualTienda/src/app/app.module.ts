import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { InputTextModule} from 'primeng/inputtext';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { SliderModule } from 'primeng/slider';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './Pages/home/home.component';
import { RegistroProductoComponent } from './Pages/registro-producto/registro-producto.component';
import { CalendarModule } from 'primeng/calendar';
import { AccordionModule } from 'primeng/accordion';
import {OverlayPanelModule} from 'primeng/overlaypanel';
import {TabViewModule} from 'primeng/tabview';
import {ToastModule} from 'primeng/toast';
import {TableModule} from 'primeng/table';
import {Sidebar, SidebarModule} from 'primeng/sidebar';

import {MessageService} from 'primeng/api';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    RegistroProductoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    InputTextModule,
    CalendarModule,
    BrowserAnimationsModule,
    SliderModule,
    FormsModule,
    AccordionModule,
    DropdownModule,
    OverlayPanelModule,
    TabViewModule,
    ToastModule,
    TableModule,
    ReactiveFormsModule,
    SidebarModule
  ],
  providers: [MessageService],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
