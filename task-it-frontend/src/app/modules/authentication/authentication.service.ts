import { Injectable} from '@angular/core';
import { User } from 'src/app/core/models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private url: string;
  private httpHeaders: HttpHeaders;
  private options: any;

  constructor(private http: HttpClient, private snackBar: MatSnackBar) {
    this.url = 'https://localhost:44384/api/User/Register';
    this.httpHeaders = new HttpHeaders();
    this.httpHeaders.set('Content-Type', 'application/json');
    this.httpHeaders.set('Access-Control-Allow-Origin', '*');

    this.options = { headers: this.httpHeaders };
  }

  registerUser(user: User) {
    this.http.post(this.url, user, this.options).subscribe(
      value => {
        this.snackBar.open('gebruiker is geregistreerd.', 'X', {
          panelClass: ['custom-ok']
        });
      }
    );
  }
}
