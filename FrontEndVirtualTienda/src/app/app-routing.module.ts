import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ActualizarInfoUsuarioComponent } from './Pages/actualizar-info-usuario/actualizar-info-usuario.component';
import { ConsultarFacturasComponent } from './Pages/consultar-facturas/consultar-facturas.component';
import { ConsultarProductosComponent } from './Pages/consultar-productos/consultar-productos.component';
import { ConsultarUsuarioComponent } from './Pages/consultar-usuario/consultar-usuario.component';
import { HomeComponent } from './Pages/home/home.component';
import { LoginComponent } from './Pages/login/login.component';
import { RealizarFacturaComponent } from './Pages/realizar-factura/realizar-factura.component';
import { RegistroProductoComponent } from './Pages/registro-producto/registro-producto.component';
import { RegistroUsuarioComponent } from './Pages/registro-usuario/registro-usuario.component';
import { VerFacturaComponent } from './Pages/ver-factura/ver-factura.component';
import { GuardGuard } from './services/guard.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: 'registroProducto',
    component: RegistroProductoComponent,
    data: { role: 'LIDER', role2: '', role3: '' },
    canActivate: [GuardGuard],
  },
  {
    path: 'registroUsuario',
    component: RegistroUsuarioComponent,
    data: { role: 'LIDER', role2: '', role3: '' },
    canActivate: [GuardGuard],
  },
  {
    path: 'consultarUsuarios',
    component: ConsultarUsuarioComponent,
    data: { role: 'LIDER', role2: '', role3: '' },
    canActivate: [GuardGuard],
  },
  {
    path: 'updateInfoUsuario/:email',
    component: ActualizarInfoUsuarioComponent,
    data: { role: 'LIDER', role2: '', role3: '' },
    canActivate: [GuardGuard],
  },
  {
    path: 'consultarProductos',
    component: ConsultarProductosComponent,
    data: { role: 'LIDER', role2: 'Ventas', role3: 'Interesado' },
    canActivate: [GuardGuard],
  },
  {
    path: 'crearFactura',
    component: RealizarFacturaComponent,
    data: { role: '', role2: 'Ventas', role3: '' },
    canActivate: [GuardGuard],
  },
  {
    path: 'consultarFacturas',
    component: ConsultarFacturasComponent,
    data: { role: '', role2: 'Ventas', role3: '' },
    canActivate: [GuardGuard],
  },
  { path: 'login', component: LoginComponent },
  {
    path: 'verFactura',
    component: VerFacturaComponent,
    data: { role: '', role2: 'Ventas', role3: '' },
    canActivate: [GuardGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
