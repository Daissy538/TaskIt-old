import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EmailVerificationService {
  private groupBaseUrl: URL;

  constructor(private http: HttpClient) {
    this.groupBaseUrl = new URL('Group', environment.baseUrlApi);
  }

  /**
   * Subscribe user to group
   * @param token the verification token
   */
  public subscribeGroup(token: string) {
    const subscribeURL = this.groupBaseUrl + '/Subscribe';
    return this.http.post(subscribeURL, { Token: token });
  }
}
