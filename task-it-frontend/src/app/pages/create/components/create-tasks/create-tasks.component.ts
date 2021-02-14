import { Component, OnInit } from '@angular/core';
import { TaskService } from 'src/app/modules/task/task.service';
import { Task } from 'src/app/core/models/task';

@Component({
  selector: 'app-create-tasks',
  templateUrl: './create-tasks.component.html',
  styleUrls: ['./create-tasks.component.scss']
})
export class CreateTasksComponent implements OnInit {
  tasks: Task[];

  constructor(private taskService: TaskService) {}

  ngOnInit() {}

  private CreateTask() {
    

    this.taskService.createTask(task).subscribe();
  }
}
