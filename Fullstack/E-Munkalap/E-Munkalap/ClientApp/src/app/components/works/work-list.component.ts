import { Component, OnInit } from '@angular/core';
import { HttpService } from './../../services/http.service';
import { WorkModel } from './../../models/work.model';

@Component({
  selector: 'app-work-list',
  templateUrl: './work-list.component.html',
  styleUrls: ['./work-list.component.css']
})
export class WorkListComponent implements OnInit {

  works: WorkModel[] = [];

  constructor(private httpService: HttpService) { }

  ngOnInit(): void {
    this.httpService.workList().subscribe(
      (data: WorkModel[]) => {
        this.works = data;
        console.log(this.works);
      },
      err => console.log(err)
    );

  }

}
