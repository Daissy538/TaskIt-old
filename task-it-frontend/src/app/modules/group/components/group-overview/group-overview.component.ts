import { Component, OnInit } from '@angular/core';
import { GroupService } from '../../group.service';
import { Group } from 'src/app/core/models/group';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
  CdkDropList
} from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-group-overview',
  templateUrl: './group-overview.component.html',
  styleUrls: ['./group-overview.component.scss']
})
export class GroupOverviewComponent implements OnInit {
  groups: Group[];
  dropList: CdkDropList[];

  constructor(
    private groupService: GroupService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit() {
    this.getGroups();
  }

  onCreateGroup() {
    this.router.navigate(['create/group']);
  }

  onDeleteGroup() {
    console.log('delete');
  }

  delete(event: CdkDragDrop<string[]>) {
    console.log('delete', event);
  }



  /**
   * Deletes group
   * On succes show snackbar + current subscribed groups
   * @param groupId the id of the group to be deleted
   */
  private deleteGroup(groupId: number) {
    this.groupService.deleteGroup(groupId).subscribe(response => {
      this.snackBar.open('Group is verwijderd', 'X', {
        panelClass: ['custom-ok']
      });

      this.groups = response;
    });
  }

  /**
   * Gets the subscribed groups of the user
   */
  private getGroups() {
    this.groupService.getGroups().subscribe(response => {
      this.groups = response;
    });
  }
}
