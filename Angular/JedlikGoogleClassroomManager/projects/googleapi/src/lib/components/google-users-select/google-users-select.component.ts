import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { GoogleApiService } from '../../googleapi.service';
declare var gapi: any;

@Component({
  selector: 'g-users-select',
  templateUrl: './google-users-select.component.html',
  styleUrls: ['./google-users-select.component.css']
})
export class GoogleUsersSelectComponent implements OnInit {
  @Input() title = 'Felhasználók kiválasztása';
  @Output() usersSelected = new EventEmitter();
  @Input() get visible(): boolean {
    return this._visible;
  }
  @Output() visibleChange = new EventEmitter();
  set visible(value: boolean) {
    this._visible = value;
    this.visibleChange.emit(value);
  }
  _visible = false;
  errorMessage = '';
  emailAddress = '';
  userList = [];
  saving = false;

  constructor(private googleApiService: GoogleApiService) { }

  ngOnInit(): void {
  }

  save() {
    // this.usersSelected.emit(this.userList);
    let emails = [];
    this.userList.filter(u => u.type === 'USER').forEach(u => emails.push(u.email));
    let groupCount = this.userList.filter(u => u.type === 'GROUP').length;
    if (groupCount === 0) {
      this.usersSelected.emit(emails);
    } else {
      this.saving = true;
      this.userList.filter(u => u.type === 'GROUP').forEach(group => {
        this.googleApiService.groupMembersEmail(group).then((result: string[]) => {
          emails = emails.concat(result);
          groupCount--;
          if (groupCount === 0) {
            this.saving = false;
            this.usersSelected.emit(emails);
          }
        });
      });
    }
  }

  userMatch(user) {
    if (this.userList.filter(u => u.email === user.email).length === 0) {
      this.userList.push(user);
      this.emailAddress = '';
    }
  }

  removeUser(user) {
    const index = this.userList.findIndex(l => l.email === user.email);
    this.userList.splice(index, 1);
  }
}
