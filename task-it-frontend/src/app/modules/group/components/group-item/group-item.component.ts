import { Component, OnInit, Input, HostListener } from '@angular/core';
import { Group } from 'src/app/core/models/group';

@Component({
  selector: 'app-group-item',
  templateUrl: './group-item.component.html',
  styleUrls: ['./group-item.component.scss']
})
export class GroupItemComponent implements OnInit {

  @Input()
  group: Group;

  screenWidth: number;

  constructor() { }

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.screenWidth = window.innerWidth;
  }

  ngOnInit() {
    this.screenWidth = window.innerWidth;
  }

}
