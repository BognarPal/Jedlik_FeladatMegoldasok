import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ConfirmComponent } from './components/confirm/confirm.component';
import {
  ProfessionsListComponent,
  EmployeeListComponent,
  NewWorkComponent,
  WorkListComponent
} from './components';

@NgModule({
  declarations: [
    AppComponent,
    ProfessionsListComponent,
    ConfirmComponent,
    EmployeeListComponent,
    NewWorkComponent,
    WorkListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
