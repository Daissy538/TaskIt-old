import { Component, OnInit, Input } from '@angular/core';
import { Task } from 'src/app/core/models/task';

@Component({
  selector: 'app-task-listItem',
  templateUrl: './task-listItem.component.html',
  styleUrls: ['./task-listItem.component.css']
})
export class TaskListItemComponent implements OnInit {
  @Input()
  task: Task;

  constructor() {}

  ngOnInit() {}
}
