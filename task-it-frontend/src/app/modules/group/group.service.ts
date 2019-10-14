import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router, ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';
import { GroupOutgoing, GroupIncoming } from 'src/app/core/models/group';
import { Observable } from 'rxjs';
import { Color } from 'src/app/core/models/color';
import { Icon } from 'src/app/core/models/Icon';

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  private baseUrl: URL;
  private redirectUrl: string;

  constructor(
    private http: HttpClient,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.baseUrl = new URL('Group', environment.baseUrlApi);
  }

  /**
   * Creates group
   * @param group the group to be created
   */
  public createGroup(group: GroupOutgoing): Observable<GroupOutgoing[]> {
    const createUrl = this.baseUrl.href + '/Create';
    return this.http.post<GroupOutgoing[]>(createUrl, group);
  }

  /**
   * Deletes group by group id
   * @param groupId the id of the group
   */
  public deleteGroup(groupId: number): Observable<GroupIncoming[]> {
    const deleteUrl = this.baseUrl.href + '/Delete/' + groupId;
    return this.http.delete<GroupIncoming[]>(deleteUrl);
  }

  /**
   * Gets all the groups where the user is subscribed on
   */
  public getGroups(): Observable<GroupIncoming[]> {
    const retrieveUrl = this.baseUrl.href + '/All';
    return this.http.get<GroupIncoming[]>(retrieveUrl);
  }

  public getGroupByID(groupID: number): Observable<GroupIncoming> {
    const retrieveUrl = this.baseUrl.href + '/' + groupID;
    return this.http.get<GroupIncoming>(retrieveUrl);
  }

  /**
   * Update the given group
   */
  public updateGroup(group: GroupOutgoing): Observable<GroupIncoming> {
    const updateUrl = this.baseUrl.href + '/Update/' + group.id;
    return this.http.post<GroupIncoming>(updateUrl, group);
  }

  /**
   * Retrieve the default colors
   */
  public getDefaultColors(): Observable<Color[]> {
    const colorUrl = environment.baseUrlApi + 'Defaults/Colors';
    return this.http.get<Color[]>(colorUrl);
  }

  public getDefaultIcons(): Observable<Icon[]> {
    const iconUrl = environment.baseUrlApi + 'Defaults/Icons';
    return this.http.get<Icon[]>(iconUrl);
  }
}
