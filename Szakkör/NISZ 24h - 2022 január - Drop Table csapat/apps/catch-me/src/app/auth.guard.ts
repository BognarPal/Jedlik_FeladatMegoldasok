import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Route,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { environment } from '../environments/environment';
import { AuthenticationService } from './services';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private authHttpService: AuthenticationService
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<boolean> | boolean {
    if (!route.routeConfig || !route.routeConfig.path) {
      return true;
    }
    const routepath = route.routeConfig.path.toLowerCase();
    if (!environment.production) {
      console.log('debug info (canActivate)', routepath);
    }
    if (routepath === 'users') {
      return this.authHttpService.hasRole('Admin');
    } else if (
      routepath === 'map' ||
      routepath === 'games' ||
      routepath === 'game'
    ) {
      if (!this.authHttpService.currentUser) {
        this.router.navigateByUrl(`/login?returnUrl=${routepath}`);
        return false;
      }
      return true;
    } else {
      return true;
    }
  }
}
