import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router, ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';
import { Group } from 'src/app/core/models/group';

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  private baseUrl: URL;
  private redirectUrl: string;

  constructor(
    private http: HttpClient,
    private snackBar: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.baseUrl = new URL('Group', environment.baseUrlApi);
  }

  /**
   * Creates group
   * @param group the group to be created
   */
  public createGroup(group: Group) {
    const createUrl = this.baseUrl.href + '/Create';
    this.http.post(createUrl, group).subscribe(response => {
      console.log(response);
    });
  }

  /**
   * Deletes group by group id
   * @param groupId the id of the group
   */
  public deleteGroup(groupId: number) {
    const deleteUrl = this.baseUrl.href + '/Create/' + groupId;
    this.http.delete(deleteUrl).subscribe(response => {
      console.log(response);
    });
  }

  /**
   * Gets all the groups where the user is subscribed on
   */
  public getGroups() {
    const retrieveUrl = this.baseUrl.href + '/All';

    this.http.get<Group[]>(retrieveUrl).subscribe(response => {
      console.log(response)
    });
  }
}
