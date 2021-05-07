import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ActualizarInfoUsuarioComponent } from './Pages/actualizar-info-usuario/actualizar-info-usuario.component';
import { ConsultarProductosComponent } from './Pages/consultar-productos/consultar-productos.component';
import { ConsultarUsuarioComponent } from './Pages/consultar-usuario/consultar-usuario.component';
import { HomeComponent } from './Pages/home/home.component';
import { RealizarFacturaComponent } from './Pages/realizar-factura/realizar-factura.component';
import { RegistroProductoComponent } from './Pages/registro-producto/registro-producto.component';
import { RegistroUsuarioComponent } from './Pages/registro-usuario/registro-usuario.component';
import { GuardGuard } from './services/guard.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'registroProducto', component: RegistroProductoComponent, data: { role: 'LIDER'}, canActivate: [GuardGuard] },
  { path: 'registroUsuario', component: RegistroUsuarioComponent, data: {role: 'LIDER'}, canActivate: [GuardGuard]},
  { path: 'consultarUsuarios', component: ConsultarUsuarioComponent, data: {role: 'LIDER'}, canActivate: [GuardGuard]},
  { path: 'updateInfoUsuario/:email', component: ActualizarInfoUsuarioComponent, data: {role: 'LIDER'}, canActivate: [GuardGuard]},
  { path: 'consultarProductos', component: ConsultarProductosComponent, data: {role: 'LIDER', role2: 'Interesado'}, canActivate: [GuardGuard]},
  { path: 'crearFactura', component: RealizarFacturaComponent, data: {role: 'Ventas'}, canActivate: [GuardGuard]},


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
