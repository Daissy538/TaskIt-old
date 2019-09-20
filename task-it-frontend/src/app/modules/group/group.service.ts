import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router, ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';
import { Group } from 'src/app/core/models/group';
import { Observable } from 'rxjs';

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
  public createGroup(group: Group): Observable<Group[]> {
    const createUrl = this.baseUrl.href + '/Create';
    return this.http.post<Group[]>(createUrl, group);
  }

  /**
   * Deletes group by group id
   * @param groupId the id of the group
   */
  public deleteGroup(groupId: number): Observable<Group[]> {
    const deleteUrl = this.baseUrl.href + '/Create/' + groupId;
    return this.http.delete<Group[]>(deleteUrl);
  }

  /**
   * Gets all the groups where the user is subscribed on
   */
  public getGroups(): Observable<Group[]> {
    const retrieveUrl = this.baseUrl.href + '/All';
    
    return this.http.get<Group[]>(retrieveUrl);
  }
}
