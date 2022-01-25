import { Component, ElementRef, NgZone, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../services';

@Component({
  selector: 'catch-me-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  @ViewChild('spanTimeOut', { static: false }) spanTimeOut?: ElementRef;
  timeToTimeout = '';
  loginQueryParams = {
    queryParams: {}
  };
  
  constructor(
    public authenticationService: AuthenticationService,
    protected zone: NgZone,
    protected router: Router) {    
  }

  ngOnInit(): void {
    this.authenticationService.currentUserChanged.subscribe(
      usr => {
        if (!usr) {
          //Ha csak bejelentkezett felhasználó férhet hozzá az oldalakhoz
          //this.router.navigate(['/login'], this.loginQueryParams);
        } else {
          this.printLoginTimeout();
        }
      });
    this.zone.runOutsideAngular(() => {
      setInterval(() => this.printLoginTimeout(), 1000);
    });

    //Ha csak bejelentkezett felhasználó férhet hozzá az oldalakhoz
    // if (!this.authenticationService.currentUser) {
    //   this.router.navigate(['/login'], this.loginQueryParams);
    // }
  }

  logout(): void {
    this.authenticationService.logout(true);
  }

  printLoginTimeout(): void {
    if (this.authenticationService.currentUser && this.authenticationService.currentUser.validTo) {
      const difInSeconds = Math.floor((this.authenticationService.currentUser.validTo.getTime() - new Date().getTime()) / 1000);
      this.timeToTimeout = '(' + ('00' + Math.floor(difInSeconds / 60).toString()).slice(-2) + ':' + ('00' + (difInSeconds % 60).toString()).slice(-2) + ')';
      if (difInSeconds <= 2) {
        this.zone.run(() => this.logout());
      }
    } else {
      this.timeToTimeout = '';
    }
    try {      
      if (this.spanTimeOut) {
        this.spanTimeOut.nativeElement.innerHTML = this.timeToTimeout;
      }
    // eslint-disable-next-line no-empty
    } catch (e) { }
  }

}

