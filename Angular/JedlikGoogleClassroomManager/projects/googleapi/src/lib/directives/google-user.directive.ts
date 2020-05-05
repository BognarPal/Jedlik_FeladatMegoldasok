import { Directive, OnInit, ElementRef, Renderer2, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { NgModel, RangeValueAccessor } from '@angular/forms';
import { GoogleApiService } from '../googleapi.service';
declare var gapi: any;

@Directive({
  // tslint:disable-next-line: directive-selector
  selector: '[g-user]'
})
export class GoogleUserDirective implements OnInit, OnChanges {
  @Input() ngModel;
  @Input('g-user') listGroups = false;
  @Output() userMatch = new EventEmitter();

  element: any;
  searchDiv: any = undefined;
  namesDiv: any = undefined;
  userList = {
    requestTime: new Date(),
    users: []
  };

  constructor(
    private elementRef: ElementRef,
    private renderer: Renderer2,
    private model: NgModel,
    private googleApiService: GoogleApiService) {
  }

  ngOnInit(): void {
    this.element = this.elementRef.nativeElement;
    this.renderer.setProperty(this.element, 'autocomplete', 'off');
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.userList.requestTime = new Date();
    this.userList.users = [];
    if (!changes.ngModel.currentValue) {
      this.removeSearchDiv();
      return;
    }
    this.searchEmail(changes.ngModel.currentValue, this.userList.requestTime);
  }

  searchEmail(emailAddress: string, requestTime: Date) {
    if (this.googleApiService.apiLoaded && gapi.auth2.getAuthInstance().isSignedIn.get()) {
      gapi.client.directory.users.list({
        customer: 'my_customer',
        maxResults: 20,
        orderby: 'email',
        query: 'email:*' + emailAddress + '*'
      }).then((response: any) => {
        if (this.userList.requestTime === requestTime) {
          (response.result.users || []).forEach(user => {
            this.userList.users.push({
              name: user.name,
              email: user.primaryEmail,
              type: 'USER'
            });
          });
          this.viewResults(this.userList.users);
        }
      });
      if (this.listGroups) {
        gapi.client.directory.groups.list({
          customer: 'my_customer',
          maxResults: 20,
          orderby: 'email',
          query: 'email:*' + emailAddress + '*'
        }).then((response: any) => {
          if (this.userList.requestTime === requestTime) {
            (response.result.groups || []).forEach(group => {
              this.userList.users.push({
                name: {
                  givenName: group.name,
                  familyName: '',
                },
                email: group.email,
                type: 'GROUP'
              });
            });
            this.viewResults(this.userList.users);
          }
        });
      }
    }
  }

  viewResults(users: any[]) {
    if (users && users.length > 0) {
      if (users.length === 1 && this.model.model === users[0].email) {
        this.removeSearchDiv();
        this.userMatch.emit(users[0]);
      } else {
        this.addSearchDiv(users.length);
        users.forEach(user => {
          this.addUserToSearchDiv(user);
        });
      }
    } else {
      this.addSearchDiv(0);
    }
  }

  addUserToSearchDiv(user: any) {
    const div = this.renderer.createElement('div');
    this.renderer.setStyle(div, 'margin', '0.5rem 0.75rem');
    this.renderer.setStyle(div, 'cursor', 'pointer');
    this.renderer.addClass(div, 'row');

    let img;
    if (user.thumbnailPhotoUrl) {
      img = this.renderer.createElement('img');
      img.src = user.thumbnailPhotoUrl;
    } else {
      img = this.renderer.createElement('div');
      this.renderer.setStyle(img, 'width', '2.5rem');
      this.renderer.setStyle(img, 'border', '1px solid #aaa');
      this.renderer.setStyle(img, 'padding', '0.55rem');
      this.renderer.setStyle(img, 'text-align', 'center');
      this.renderer.setStyle(img, 'background', 'darkcyan');
      this.renderer.setStyle(img, 'color', 'antiquewhite');
      this.renderer.appendChild(img, this.renderer.createText(user.name.givenName.substring(0, 1)));
    }
    this.renderer.setStyle(img, 'float', 'left');
    this.renderer.setStyle(img, 'height', '2.5rem');
    this.renderer.setStyle(img, 'border-radius', '2.5rem');
    this.renderer.setStyle(img, 'margin-right', '0.75rem');

    const dataDiv = this.renderer.createElement('div');
    this.renderer.setStyle(dataDiv, 'float', 'left');
    this.renderer.setStyle(dataDiv, 'height', '2.5rem');

    const nameDiv = this.renderer.createElement('h5');
    this.renderer.setStyle(nameDiv, 'margin-bottom', '0');
    this.renderer.appendChild(nameDiv, this.renderer.createText(user.name.familyName + ' ' + user.name.givenName));
    this.renderer.appendChild(dataDiv, nameDiv);

    const emailDiv = this.renderer.createElement('p');
    this.renderer.appendChild(emailDiv, this.renderer.createText(user.email));
    this.renderer.appendChild(dataDiv, emailDiv);

    this.renderer.appendChild(div, img);
    this.renderer.appendChild(div, dataDiv);
    this.renderer.appendChild(this.namesDiv, div);

    this.renderer.listen(div, 'click', (event) => {
      this.model.viewToModelUpdate(user.email);
      this.model.valueAccessor.writeValue(user.email);
      this.userMatch.emit(user);
      this.removeSearchDiv();
    });
  }

  addSearchDiv(length: number) {
    this.removeSearchDiv();

    this.searchDiv = this.renderer.createElement('div');
    this.renderer.setStyle(this.searchDiv, 'background-color', '#f7f7f7');
    this.renderer.setStyle(this.searchDiv, 'position', 'fixed');
    this.renderer.setStyle(this.searchDiv, 'width', this.element.clientWidth + 'px');
    this.renderer.setStyle(this.searchDiv, 'height', (4 + (length > 5 ? 5 : length) * 3).toString() + 'rem');
    this.renderer.setStyle(this.searchDiv, 'border', '1px solid #aaa');
    this.renderer.setStyle(this.searchDiv, 'border-radius', '6px');
    this.renderer.setStyle(this.searchDiv, 'margin-top', this.element.offsetHeight + 2 + 'px');
    this.renderer.setStyle(this.searchDiv, 'z-index', this.maxZIndex() + 1);

    const title = this.renderer.createElement('p');
    this.renderer.setStyle(title, 'margin', '0.75rem');
    this.renderer.appendChild(title, this.renderer.createText('Keresési találatok'));
    this.renderer.appendChild(this.searchDiv, title);

    this.namesDiv = this.renderer.createElement('div');
    this.renderer.setStyle(this.namesDiv, 'max-height', '16rem');
    if (length > 5) {
      this.renderer.setStyle(this.namesDiv, 'overflow-y', 'auto');
    }
    this.renderer.appendChild(this.searchDiv, this.namesDiv);

    this.renderer.insertBefore(this.element.parentNode, this.searchDiv, this.element);
  }

  removeSearchDiv() {
    if (this.searchDiv) {
      this.renderer.removeChild(this.element.parentNode, this.searchDiv);
      this.searchDiv = undefined;
    }
  }

  maxZIndex() {
    const maxIndex = Array.from(document.querySelectorAll('body *'))
      .filter(a => !a.classList.contains('app-waiting'))
      .map(a => parseFloat(window.getComputedStyle(a).zIndex))
      .filter(a => !isNaN(a))
      .sort((a, b) => Number(a) - Number(b))
      .pop();
    return maxIndex < 0 ? 0 : maxIndex;
  }

}
