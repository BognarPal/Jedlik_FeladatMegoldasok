import { Component, OnInit } from '@angular/core';
import { HttpService } from './../../services/http.service';
import { WorkModel } from './../../models/work.model';
import { ProfessionModel } from './../../models/profession.model';
import { EmployeeModel } from './../../models/employee.model';

@Component({
  selector: 'app-work-list',
  templateUrl: './work-list.component.html',
  styleUrls: ['./work-list.component.css']
})
export class WorkListComponent implements OnInit {

  works: WorkModel[] = [];
  employees: EmployeeModel[] = [];
  professions: ProfessionModel[] = [];
  setEmployee = {
    visible: false,
    model: new WorkModel(),
    missing : {
      profession: false,
      employee: false,
      deadline: false,
      name: false
    }
  }

  workFinished = {
    visible: false,
    model: new WorkModel(),
    missing : {
      date: false
    }
  }

  workChecked = {
    visible: false,
    model: new WorkModel(),
    missing : {
      user: false
    }
  }

  constructor(private httpService: HttpService) { }

  ngOnInit(): void {
    this.httpService.workList().subscribe(
      (data: WorkModel[]) => {
        this.works = data;
        console.log(this.works);
      },
      err => console.log(err)
    );

    this.httpService.getEmployees().subscribe (
      (data: EmployeeModel[]) => {
        this.employees = data;
      },
      (err: any) => console.log(err)
    );

    this.httpService.getProfessions().subscribe (
      (data: ProfessionModel[]) => {
        this.professions = data;
      },
      err => console.log(err)
    );

  }
  workSetEmployee(id?: Number): void {
    this.setEmployee.visible = true;
    this.setEmployee.model = new WorkModel();
    this.setEmployee.model.id = id;
  }

  workSetFinished(id?: Number): void {
    this.workFinished.visible = true;
    this.workFinished.model = new WorkModel();
    this.workFinished.model.id = id;
  }

  workSetChecked(id?: Number): void {
    this.workChecked.visible = true;
    this.workChecked.model = new WorkModel();
    this.workChecked.model.id = id;
    const index = this.works.findIndex(w => w.id === id);
    this.workChecked.model.checkerUser = this.works[index].assignerName;
  }

  workSetEmployeeSave(): void {
    this.setEmployee.missing.profession = !Number(this.setEmployee.model.professionId);
    this.setEmployee.missing.employee = !Number(this.setEmployee.model.employeeId);
    this.setEmployee.missing.deadline = !this.setEmployee.model.deadLine;
    this.setEmployee.missing.name = !this.setEmployee.model.assignerName;

    if (!this.setEmployee.missing.profession &&
        !this.setEmployee.missing.employee &&
        !this.setEmployee.missing.deadline &&
        !this.setEmployee.missing.name) {

      const data = {
        id: this.setEmployee.model.id,
        professionId: Number(this.setEmployee.model.professionId),
        employeeId: Number(this.setEmployee.model.employeeId),
        deadLine: this.setEmployee.model.deadLine,
        assignDetails: this.setEmployee.model.assignDetails,
        assignerName: this.setEmployee.model.assignerName
      }

      this.httpService.workSetEmployee(data).subscribe(
        (data: WorkModel) => {
          const index = this.works.findIndex(w => w.id === data.id);
          this.works[index] = data; 
          this.setEmployee.visible = false;         
        },
        err => console.log(err)
      )
    }

  }

  workSetFinishedSave(): void {
    this.workFinished.missing.date = !this.workFinished.model.finishDate;
    if (!this.workFinished.missing.date) {
      const data = {
        id: this.workFinished.model.id,
        finishDate: this.workFinished.model.finishDate,
        finishComment: this.workFinished.model.finishComment
      }
      this.httpService.workFinished(data).subscribe(
        (data: WorkModel) => {
          const index = this.works.findIndex(w => w.id === data.id);
          this.works[index] = data; 
          this.workFinished.visible = false;         
        },
        err => console.log(err)
      )
    }
  }

  workCheckedSave(): void {
    this.workChecked.missing.user = !this.workChecked.model.checkerUser;
    if (!this.workChecked.missing.user) {
      const data = {
        id: this.workChecked.model.id,
        checkerUser: this.workChecked.model.checkerUser,
        checkComment: this.workChecked.model.checkComment
      }
      this.httpService.workChecked(data).subscribe(
        (data: WorkModel) => {
          const index = this.works.findIndex(w => w.id === data.id);
          this.works[index] = data; 
          this.workChecked.visible = false;         
        },
        err => console.log(err)
      )
    }
  }
}

