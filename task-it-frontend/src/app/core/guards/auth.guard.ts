import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router
} from '@angular/router';
import { AuthenticationService } from 'src/app/modules/authentication/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthenticationService,
    private router: Router
  ) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    const url = state.url;

    return this.checkLogin(url);
  }

  private checkLogin(url: string): boolean {
    const token = this.authService.getToken();

    const expirationDate = this.authService.getExpirationDate();
    const currentTime = Date.now() / 1000;

    if (token && expirationDate > currentTime) {
      return true;
    }

    this.authService.setRedirectUrl(url);
    this.authService.logOut();

    console.log('check');
    this.router.navigate(['auth/login']);
    return false;
  }
}
