import { Injectable } from '@angular/core';
import { User } from 'src/app/core/models/user';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from 'src/environments/environment';
import * as jwt_decode from 'jwt-decode';
import { Router, ActivatedRoute } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private baseUrl: URL;
  private redirectUrl: string;

  constructor(
    private http: HttpClient,
    private snackBar: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.baseUrl = new URL('User', environment.baseUrlApi);
  }

  /**
   * Registers user
   * @param user User to be registered
   */
  registerUser(user: User) {
    const urlRegister = this.baseUrl.href + '/Register';

    this.http.post(urlRegister, user).subscribe(value => {
      this.snackBar.open('gebruiker is geregistreerd.', 'X', {
        panelClass: ['custom-ok']
      });
    });
  }

  /**
   * Logins user and retrieve a jwt token
   * @param user a user with password and email
   */
  loginUser(user: User) {
    const urlLogin = this.baseUrl.href + '/Auth';

    this.http.post(urlLogin, user).subscribe(value => {
      this.setSession(value['token']);
      if (this.redirectUrl) {
        this.router.navigateByUrl(this.redirectUrl);
        return;
      }

      this.router.navigate(['../../dashboard'], {relativeTo: this.route} );
    });
  }

  /**
   * Logs out and clear all localStorage data.
   */
  logOut() {
    localStorage.clear();
    this.router.navigate(['../auth/login'], {relativeTo: this.route} );
  }

  /**
   * Set redirect url.
   * This url will be used when the user is loggedin again.
   * @param url old url
   */
  public setRedirectUrl(url: string) {
    this.redirectUrl = url;
  }

  /**
   * Checks if there is a session active
   */
  public isLoggedIn(): boolean {
    return localStorage.getItem('token') !== null;
  }

  /**
   * Get token of the active session
   */
  public getToken(): string {
    return localStorage.getItem('token');
  }

  /**
   * Get expiration date of the active session
   */
  public getExpirationDate(): number {
    const expDate = localStorage.getItem('expirationDate');
    if (expDate) {
      return parseFloat(expDate);
    }

    return null;
  }

  private setSession(jwtToken: string) {
    const token = jwt_decode(jwtToken);

    localStorage.setItem('id', token['id']);
    localStorage.setItem('token', jwtToken);
    localStorage.setItem('expirationDate', token['exp']);
  }
}
