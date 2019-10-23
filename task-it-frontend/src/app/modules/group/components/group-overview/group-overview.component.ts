import {
  Component,
  OnInit,
  HostListener
} from '@angular/core';
import { GroupService } from '../../group.service';
import { GroupOutgoing, GroupIncoming } from 'src/app/core/models/group';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CdkDragDrop } from '@angular/cdk/drag-drop';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/core/components/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-group-overview',
  templateUrl: './group-overview.component.html',
  styleUrls: ['./group-overview.component.scss']
})
export class GroupOverviewComponent implements OnInit {
  groups: GroupIncoming[];
  inDraggingModus: boolean;
  screenWidth: number;

  constructor(
    private dialog: MatDialog,
    private groupService: GroupService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.screenWidth = window.innerWidth;
  }

  ngOnInit() {
    this.getGroups();
    this.inDraggingModus = false;
    this.screenWidth = window.innerWidth;
  }

  onCreateGroup() {
    this.router.navigate(['create/group']);
  }

  /**
   * Deletes group element
   * @param event  the drop event
   */
  delete(event: CdkDragDrop<string[]>) {
    const index = Number(event.item.data);
    const group = this.groups[index];
    this.deleteGroup(group);
  }

  /**
   * Navigate to group details
   * @param group the outgoing groupdetails
   */
  groupDetails(group: GroupOutgoing) {
    if (!this.inDraggingModus) {
      this.router.navigate(['details/group/', group.id]);
    }
  }

  /**
   * Determines whether element is dragging
   * @param isDragging true if dragging, false otherwise
   */
  isDragging(isDragging: boolean) {
    this.inDraggingModus = isDragging;
  }

  /**
   * Deletes group
   * On succes show snackbar + current subscribed groups
   * @param groupId the id of the group to be deleted
   */
  private deleteGroup(groupSelected: GroupIncoming) {
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
