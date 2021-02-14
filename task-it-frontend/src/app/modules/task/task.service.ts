import { Injectable } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';
import { TaskOutgoing, Task } from 'src/app/core/models/task';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private baseUrl: URL;
  private redirectUrl: string;

  constructor(
    private http: HttpClient,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.baseUrl = new URL('Task', environment.baseUrlApi);
  }

  public createTask(task: TaskOutgoing): Observable<Task[]> {
    const createUrl = this.baseUrl.href + '/Create';

    return this.http.post<Task[]>(createUrl, task);
  }
}
