import { Component, OnInit } from '@angular/core';
import { HttpService } from './../../services/http.service';
import { ProfessionModel } from './../../models/profession.model';

@Component({
  selector: 'app-professions-list',
  templateUrl: './professions-list.component.html',
  styleUrls: ['./professions-list.component.css']
})
export class ProfessionsListComponent implements OnInit {

  professions: ProfessionModel[] = [];
  isListState = true;
  modifyRecord: ProfessionModel = {
    id: 0,
    name: ''
  };
  torles = {
    dialog: false,
    model: new ProfessionModel()
  }

  constructor(private httpService: HttpService) { }

  ngOnInit(): void {
    this.httpService.getProfessions().subscribe (
      (data: ProfessionModel[]) => {
        this.professions = data;
      },
      err => console.log(err)
    );
  }

  modify(prof: ProfessionModel): void {
    this.isListState = false;
    this.modifyRecord.id = prof.id;
    this.modifyRecord.name = prof.name;
  }

  delete(prof: ProfessionModel): void {
    this.torles.dialog = true;
    this.torles.model = prof;
  }

  confirmedDelete(): void {
    this.httpService.deleteProfessions(this.torles.model).subscribe (
      (data: boolean) => {
        if (data) {
          const index = this.professions.findIndex(p => p.id === this.torles.model.id);
          this.professions.splice(index, 1);
          this.torles.dialog = false;
        }
      }
    );
  }

  new(): void {
    this.isListState = false;
    this.modifyRecord = {
      id: -1,
      name: ''
    };
    this.professions.unshift(this.modifyRecord);    
  }

  save(): void {
    const index = this.professions.findIndex(p => p.id === this.modifyRecord.id);
    if (this.modifyRecord.id === -1) {   //új
      this.httpService.newProfessions(this.modifyRecord).subscribe(
        (data: ProfessionModel) => {
          this.cancel();
          this.professions.unshift(data);
        }
      );
    }   
    else {                               //módosítás
      this.httpService.modifyProfessions(this.modifyRecord).subscribe(
        (data: ProfessionModel) => {
          this.professions[index].name = data.name;
          this.cancel();
        }
      );
    }
  }

  cancel(): void {
    if (this.professions[0].id === -1) {
      this.professions.splice(0, 1);
    }
    this.isListState = true;
    this.modifyRecord.id = 0;
  }

  change(event: any): void {
    this.modifyRecord.name = event.target.value;
  }

}
