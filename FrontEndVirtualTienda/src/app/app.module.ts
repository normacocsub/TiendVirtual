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

import { CalendarModule } from 'primeng/calendar';
import { AccordionModule } from 'primeng/accordion';
import {OverlayPanelModule} from 'primeng/overlaypanel';
import {TabViewModule} from 'primeng/tabview';
import {ToastModule} from 'primeng/toast';
import {TableModule} from 'primeng/table';
import {Sidebar, SidebarModule} from 'primeng/sidebar';
import {PasswordModule} from 'primeng/password';
import {ToolbarModule} from 'primeng/toolbar';
import {SplitButtonModule} from 'primeng/splitbutton';
import {PanelModule} from 'primeng/panel';
import {DataViewModule} from 'primeng/dataview';
import {DynamicDialogModule} from 'primeng/dynamicdialog';
import {CardModule} from 'primeng/card';
import {SplitterModule} from 'primeng/splitter';
import {PickListModule} from 'primeng/picklist';

import {MessageService} from 'primeng/api';
import {DialogService} from 'primeng/dynamicdialog';

import { RegistroProductoComponent } from './Pages/registro-producto/registro-producto.component';
import { RegistroUsuarioComponent } from './Pages/registro-usuario/registro-usuario.component';
import { ConsultarUsuarioComponent } from './Pages/consultar-usuario/consultar-usuario.component';
import { ActualizarInfoUsuarioComponent } from './Pages/actualizar-info-usuario/actualizar-info-usuario.component';
import { ConsultarProductosComponent } from './Pages/consultar-productos/consultar-productos.component';
import { VerProductoComponent } from './Pages/ver-producto/ver-producto.component';
import { RealizarFacturaComponent } from './Pages/realizar-factura/realizar-factura.component';
import { DetallesProductoCompraComponent } from './Pages/detalles-producto-compra/detalles-producto-compra.component';






@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    RegistroProductoComponent,
    RegistroUsuarioComponent,
    ConsultarUsuarioComponent,
    ActualizarInfoUsuarioComponent,
    ConsultarProductosComponent,
    VerProductoComponent,
    RealizarFacturaComponent,
    DetallesProductoCompraComponent
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
    SidebarModule,
    PasswordModule,
    ToolbarModule,
    SplitButtonModule,
    PanelModule,
    DataViewModule,
    DynamicDialogModule,
    CardModule,
    SplitterModule,
    PickListModule
  ],
  providers: [
    MessageService,
    DialogService
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
