import { Injectable } from '@angular/core';
import { User } from 'src/app/core/models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from 'src/environments/environment';
import * as jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private baseUrl: URL;
  private httpHeaders: HttpHeaders;
  private options: any;

  constructor(private http: HttpClient, private snackBar: MatSnackBar) {
    this.baseUrl = new URL('User', environment.baseUrlApi);
    this.httpHeaders = new HttpHeaders();
    this.httpHeaders.set('Content-Type', 'application/json');
    this.httpHeaders.set('Access-Control-Allow-Origin', '*');

    this.options = { headers: this.httpHeaders };
  }

  /**
   * Registers user
   * @param user User to be registered
   */
  registerUser(user: User) {
    const urlRegister = this.baseUrl.href + '/Register';

    this.http.post(urlRegister, user, this.options).subscribe(value => {
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
    this.http.post(urlLogin, user, this.options).subscribe(value => {
      this.setSession(value['token']);
      this.snackBar.open('gebruiker is ingelogd.', 'X', {
        panelClass: ['custom-ok']
      });
    });
  }

  /**
   * Logs out and clear all localStorage data.
   */
  logOut() {
    localStorage.clear();
  }

  /**
   * Checks if there is a session active
   */
  public isLoggedIn(): boolean {
    return localStorage.getItem('token') === null;
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
