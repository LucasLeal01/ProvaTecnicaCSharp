import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FuncionariosListComponent } from './components/funcionarios-list/funcionarios-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/funcionarios', pathMatch: 'full' },
  { path: 'funcionarios', component: FuncionariosListComponent },
  { 
    path: 'ferias', 
    loadComponent: () => import('./components/ferias/ferias.component').then(m => m.FeriasComponent) 
  },
  { path: '**', redirectTo: '/funcionarios' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }