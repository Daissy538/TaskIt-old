import { Component, OnInit, Input } from '@angular/core';
import { Task } from 'src/app/core/models/task';

@Component({
  selector: 'app-taks-list',
  templateUrl: './taks-list.component.html',
  styleUrls: ['./taks-list.component.css']
})
export class TaksListComponent implements OnInit {

  @Input()
  tasks: Task[];

  constructor() { }

  ngOnInit() {
  }

}
