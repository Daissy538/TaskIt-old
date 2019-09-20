import { Component, OnInit, Input } from '@angular/core';
import { Group } from 'src/app/core/models/group';

@Component({
  selector: 'app-group-item',
  templateUrl: './group-item.component.html',
  styleUrls: ['./group-item.component.scss']
})
export class GroupItemComponent implements OnInit {

  @Input()
  group: Group;

  constructor() { }

  ngOnInit() {
  }

}
