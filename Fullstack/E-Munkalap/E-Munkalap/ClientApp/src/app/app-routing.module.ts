import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import {
  ProfessionsListComponent,
  EmployeeListComponent,
  NewWorkComponent,
  WorkListComponent
} from './components';

const routes: Routes = [
  { path: "professions", component: ProfessionsListComponent },
  { path: "employees", component: EmployeeListComponent },
  { path: "newwork", component: NewWorkComponent },
  { path: "works", component: WorkListComponent },
  { path: '', redirectTo: '/newwork', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
