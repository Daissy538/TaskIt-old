import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
  HttpEvent,
  HTTP_INTERCEPTORS
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private snackBar: MatSnackBar) {}

  intercept(
    request: HttpRequest<any>,
    handler: HttpHandler
  ): Observable<HttpEvent<any>> {
    return handler.handle(request).pipe(
      catchError(error => {
        if (error instanceof HttpErrorResponse) {
          let errorMessage = '';
          if (error.error instanceof ErrorEvent) {
            errorMessage = `Error: ${error.message}`;
          } else {
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.error}`;
          }

          this.snackBar.open(error.error, 'X', {
            panelClass: ['custom-error']
          });
          return throwError(errorMessage);
        }
      })
    );
  }
}

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true
};
