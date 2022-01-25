import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from './../../services';

@Component({
  selector: 'catch-me-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  formdata = {
    email: 'admin@droptable.jedlik.local',
    password: 'admin',
    loading: false,
    error: ''
  };
  returnUrl = '';

  constructor(
    private authenticationService: AuthenticationService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    if (this.authenticationService.currentUser) {
      this.router.navigate(['/']);
    } else {
      this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }
  }

  submit(): void {
    this.formdata.error = '';
    this.formdata.loading = true;

    if (this.formdata.email && this.formdata.password) {
      this.authenticationService.login(this.formdata.email, this.formdata.password)
        .subscribe(
          data => {
            this.router.navigateByUrl(this.returnUrl);
          },
          err => {
            this.formdata.error = err.error.message;
            this.formdata.loading = false;
          }
        );
    }
  }
}
