import { Component, Input, NgZone, AfterViewInit } from '@angular/core';
declare var gapi: any;

@Component({
  selector: 'g-user-info',
  templateUrl: './google-user-information.component.html',
  styleUrls: ['./google-user-information.component.css']
})
export class GoogleUserInformationComponent implements AfterViewInit {
  @Input() set email(value) {
    this.getUserInfo(value);
  }

  model = {
    email: '',
    isAdmin: false,
    lastLoginTime: null,
    name: '',
    phones: [],
    photoUrl: null
  };

  constructor(private ngZone: NgZone) { }

  ngAfterViewInit(): void {
  }

  getUserInfo(value: any) {
    this.model.email = '';
    this.model.isAdmin = false;
    this.model.lastLoginTime = '';
    this.model.name = null;
    this.model.phones = [];
    this.model.photoUrl = '';

    if (value) {
      gapi.client.directory.users.get({
        userKey: value
      }).then((response: any) => {
        // console.log(response.result);
        this.ngZone.run(() => {
          this.model.email = response.result.primaryEmail;
          this.model.isAdmin = response.result.isAdmin;
          this.model.lastLoginTime = response.result.lastLoginTime;
          this.model.name = response.result.name;
          this.model.phones = response.result.phones;
          this.model.photoUrl = response.result.thumbnailPhotoUrl;
        });
      });
    }
  }


}
