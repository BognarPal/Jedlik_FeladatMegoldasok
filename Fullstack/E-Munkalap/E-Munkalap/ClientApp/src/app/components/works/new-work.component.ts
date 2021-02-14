import { Component, OnInit } from '@angular/core';
import { WorkModel } from './../../models/work.model';
import { HttpService } from './../../services/http.service';

@Component({
  selector: 'app-new-work',
  templateUrl: './new-work.component.html',
  styleUrls: []
})
export class NewWorkComponent implements OnInit {

  constructor(private httpService: HttpService) { }

  work: WorkModel = new WorkModel(); 

  ngOnInit(): void {
  }

  save(): void {
    if (this.work.requesterName && this.work.description) {
      this.httpService.newWork(this.work).subscribe(
        (data: WorkModel) => {
          this.work = new WorkModel();
          alert(`Az ön által rögzített igény száma: ${data.id}`);
        },
        err => console.log(err)
      )

    }
  }
}
