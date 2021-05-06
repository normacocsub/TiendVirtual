import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Pages/home/home.component';
import { RegistroProductoComponent } from './Pages/registro-producto/registro-producto.component';
import { GuardGuard } from './services/guard.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'registroProducto', component: RegistroProductoComponent, data: { role: 'LIDER'}, canActivate: [GuardGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
