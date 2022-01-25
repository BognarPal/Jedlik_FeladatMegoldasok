import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const usrJson = localStorage.getItem('currentUser');
    if (usrJson) {
      const usr = JSON.parse(usrJson);
      if (usr && usr.token) {
        const cloned = request.clone({
          setHeaders: { Authorization: usr.token }
        });
        return next.handle(cloned)
          .pipe(
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            tap((event: HttpEvent<any>) => {
              if (event instanceof HttpResponse && event.status === 200) {
                const user = (event.headers.get('session'));
                if (user) {
                  localStorage.setItem('currentUser', user);
                }
              }
              return event;
            })
          );
      }
    }
    return next.handle(request);
  }
}
