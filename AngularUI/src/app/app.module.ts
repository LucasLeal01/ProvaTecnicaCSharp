import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { FuncionariosListComponent } from './components/funcionarios-list/funcionarios-list.component';
import { FuncionarioFormComponent } from './components/funcionario-form/funcionario-form.component';
import { NotificationComponent } from './components/notification/notification.component';

@NgModule({
  declarations: [
    AppComponent,
    FuncionariosListComponent,
    FuncionarioFormComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule,
    NotificationComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

