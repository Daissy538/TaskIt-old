import { Injectable } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';
import { InviteOutgoingDTO } from 'src/app/core/models/email';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private groupBaseUrl: URL;

  constructor(
    private http: HttpClient,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.groupBaseUrl = new URL('Group', environment.baseUrlApi);
  }

  /**
   * Send invite user request
   * @param inviteUserData the dto with the invited user data
   * @param groupID The group
   */
  public inviteUser(
    inviteUserData: InviteOutgoingDTO,
    groupID: number
  ): Observable<any> {
    const inviteUserUrl =
      this.groupBaseUrl.toString() + '/' + groupID + '/Invite';

    return this.http.post(inviteUserUrl, inviteUserData);
  }
}
