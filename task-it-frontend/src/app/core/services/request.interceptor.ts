import { Injectable } from '@angular/core';
import { AuthenticationService } from 'src/app/modules/authentication/authentication.service';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

/**
 * Injectable for adding the bearer token to a http request.
 */
@Injectable()
export class RequestInterceptor implements HttpInterceptor {
  constructor(private autService: AuthenticationService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (this.autService.isLoggedIn()) {
      request = request.clone({
        setHeaders: {
          Authorization: 'Bearer ' + this.autService.getToken()
        }
      });

      request = request.clone({
        setHeaders: {
          'Content-Type': 'application/json'
        }
      });
    }

    return next.handle(request);
  }
}
