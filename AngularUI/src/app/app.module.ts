import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { FuncionariosListComponent } from './components/funcionarios-list/funcionarios-list.component';
import { FuncionarioFormComponent } from './components/funcionario-form/funcionario-form.component';

@NgModule({
  declarations: [
    AppComponent,
    FuncionariosListComponent,
    FuncionarioFormComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

