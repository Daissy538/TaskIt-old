import {
  Component,
  OnInit,
  QueryList,
  ViewChildren
} from '@angular/core';
import { GroupService } from '../../group.service';
import { Group } from 'src/app/core/models/group';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import {
  CdkDragDrop,
  moveItemInArray,
  CdkDropList,
  CdkDragEnter
} from '@angular/cdk/drag-drop';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/core/components/confirmation-dialog/confirmation-dialog.component';


@Component({
  selector: 'app-group-overview',
  templateUrl: './group-overview.component.html',
  styleUrls: ['./group-overview.component.scss']
})
export class GroupOverviewComponent implements OnInit {
  groups: Group[];
  dropList: CdkDropList[];
  inDraggingModus: boolean;

  constructor(
    private dialog: MatDialog,
    private groupService: GroupService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  @ViewChildren(CdkDropList) dropsQuery: QueryList<CdkDropList>;

  ngOnInit() {
    this.getGroups();
    this.inDraggingModus = false;
  }

  onCreateGroup() {
    this.router.navigate(['create/group']);
  }

  delete(event: CdkDragDrop<string[]>) {
    const index = Number(event.item.data);
    const group = this.groups[index];
    this.deleteGroup(group);
  }

  entered($event: CdkDragEnter) {
    if ($event.container.id !== 'deleteList') {
      moveItemInArray(this.groups, $event.item.data, $event.container.data);
    }
  }

  isDragging(isDragging: boolean) {
    this.inDraggingModus = isDragging;
  }

  /**
   * Deletes group
   * On succes show snackbar + current subscribed groups
   * @param groupId the id of the group to be deleted
   */
  private deleteGroup(groupSelected: Group) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {
      autofocus: false,
      description:
        'Weet je zeker dat je "' + groupSelected.name + '" wilt verwijderen?',
      title: 'Verwijderen',
      cancelButtonText: 'Nee',
      confirmButtonText: 'Ja'
    };
    const dialog = this.dialog.open(ConfirmationDialogComponent, dialogConfig);
    dialog.afterClosed().subscribe(data => {
      if (data) {
        this.groupService.deleteGroup(groupSelected.id).subscribe(response => {
          this.snackBar.open('Group is verwijderd', 'X', {
            panelClass: ['custom-ok']
          });
          this.groups = response;
        });
      }
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
