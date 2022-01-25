import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RegistrationModel } from '../../../../../../libs/my-ts-lib/src/auth';
import { AuthenticationService } from '../../services';

@Component({
  selector: 'catch-me-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['../login/login.component.scss']
})
export class RegistrationComponent {

  formdata = new RegistrationModel();
  loading = false;
  error = '';

  constructor(
    private authenticationService: AuthenticationService,
    private route: ActivatedRoute,
    private router: Router) { }

  submit(): void {
    this.error = '';
    if (this.formdata.name && this.formdata.email && this.formdata.password && this.formdata.passwordAgain) {
      const passwordCheck = this.formdata.isPasswordOk();
      if (!passwordCheck.ok) {
        this.error = passwordCheck.error || 'Hibás jelszó';
        return;
      }
      this.loading = true;
      this.authenticationService.registration(this.formdata)
        .subscribe(
          data => {
            this.router.navigateByUrl('/');
          },
          err => {
            this.error = err.error.message;
            this.loading = false;
          }
        );
    }
  }

}
