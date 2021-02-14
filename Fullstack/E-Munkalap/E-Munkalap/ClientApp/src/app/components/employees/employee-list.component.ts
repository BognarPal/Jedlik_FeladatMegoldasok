import { Component, OnInit } from '@angular/core';
import { HttpService } from 'src/app/services/http.service';
import { EmployeeModel } from './../../models/employee.model';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employees: EmployeeModel[] = [];
  isListState = true;
  modifyRecord = new EmployeeModel();
  torles = {
    dialog: false,
    model: new EmployeeModel()
  }

  constructor(private httpService: HttpService) { }

  ngOnInit(): void {
    this.httpService.getEmployees().subscribe (
      (data: EmployeeModel[]) => {
        this.employees = data;
      },
      (err: any) => console.log(err)
    );
  }

  modify(empl: EmployeeModel): void {
    this.isListState = false;
    this.modifyRecord.id = empl.id;
    this.modifyRecord.name = empl.name;
    this.modifyRecord.adLoginName = empl.adLoginName;
  }

  delete(empl: EmployeeModel): void {
    this.torles.dialog = true;
    this.torles.model = empl;
  }

  confirmedDelete(): void {
    this.httpService.deleteEmployee(this.torles.model).subscribe (
      (data: boolean) => {
        if (data) {
          const index = this.employees.findIndex(p => p.id === this.torles.model.id);
          this.employees.splice(index, 1);
          this.torles.dialog = false;
        }
      }
    );
  }

  new(): void {
    this.isListState = false;
    this.modifyRecord = {
      id: -1,
      name: '',
      adLoginName: ''
    };
    this.employees.unshift(this.modifyRecord);    
  }

  save(): void {
    const index = this.employees.findIndex(p => p.id === this.modifyRecord.id);
    if (this.modifyRecord.id === -1) {   //új
      this.httpService.newEmployee(this.modifyRecord).subscribe(
        (data: EmployeeModel) => {
          this.cancel();
          this.employees.unshift(data);
        }
      );
    }   
    else {                               //módosítás
      this.httpService.modifyEmployee(this.modifyRecord).subscribe(
        (data: EmployeeModel) => {
          this.employees[index].name = data.name;
          this.employees[index].adLoginName = data.adLoginName;
          this.cancel();
        }
      );
    }
  }

  cancel(): void {
    if (this.employees[0].id === -1) {
      this.employees.splice(0, 1);
    }
    this.isListState = true;
    this.modifyRecord.id = 0;
  }

  changeName(event: any): void {
    this.modifyRecord.name = event.target.value;
  }

  changeAdLoginName(event: any): void {
    this.modifyRecord.adLoginName = event.target.value;
  }

}
