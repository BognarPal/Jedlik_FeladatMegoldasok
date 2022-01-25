import { EventEmitter, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { UserModel } from '../models/user.model';
import { LoginResponseModel, RegistrationModel } from '../../../../../libs/my-ts-lib/src';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  public currentUserChanged = new EventEmitter();

  constructor(private http: HttpClient) {
    this.checktoken();
  }

  public get currentUser(): UserModel | undefined {
    const result = new UserModel();
    result.loadFromLocalStorage();
    if (result.name) {
      return result;
    }
    return undefined;
  }

  login(email: string, password: string): Observable<LoginResponseModel> {
    return this.http.post<LoginResponseModel>(`${environment.apiUrl}/auth/login`, { email, password })
      .pipe(map(user => {
        if (user && user.token) {
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserChanged.emit(this.currentUser);
        }
        return user;
      }));
  }

  registration(model: RegistrationModel): Observable<LoginResponseModel> {
    return this.http.post<LoginResponseModel>(`${environment.apiUrl}/auth/registration`, model )
      .pipe(map(user => {
        if (user && user.token) {
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserChanged.emit(this.currentUser);
        }
        return user;
      }));
  }

  // passwordChange(userId: string, oldpassword: string, password: string): Observable<any> {
  //   return this.http.post<any>(`${environment.apiUrl}/Auth/passwordchange`, { userId, oldpassword, password })
  //     .pipe(map(user => {
  //       if (user && user.token) {
  //         localStorage.setItem('currentUser', JSON.stringify(user));
  //         this.currentUserChanged.emit(this.currentUser);
  //       }
  //       return user;
  //     }));
  // }

  logout(fromBackend: boolean): void {
    if (this.currentUser && this.currentUser.token) {
      const header = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': this.currentUser.token
      });
      localStorage.removeItem('currentUser');
      this.currentUserChanged.emit(null);

      if (fromBackend) {
        this.http.post(`${environment.apiUrl}/auth/logout`, { headers: header }).subscribe(
          () => {
            this.currentUserChanged.emit(this.currentUser);
          },
          err => {
            console.log(err);
          }
        );
      }
    }
  }

  checktoken(): void {
    if (this.currentUser && this.currentUser.token) {
      const header = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': this.currentUser.token
      });

      this.http.get(`${environment.apiUrl}/auth/checktoken`, { headers: header }).subscribe(
        () => {
          this.currentUserChanged.emit(this.currentUser);
        },
        err => {
          this.logout(false);
        }
      );
    } else {
      this.currentUserChanged.emit(null);
    }
  }

  hasRole(role: string): boolean {
    const user = this.currentUser;
    if (user) {
      return user.roles.indexOf(role) !== -1;
    } else {
      return false;
    }
  }
}
