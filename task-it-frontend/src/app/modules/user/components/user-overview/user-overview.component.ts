import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Member } from 'src/app/core/models/member';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { UserInviteDialogComponent } from '../user-invite-dialog/user-invite-dialog.component';
import { InviteOutgoingDTO } from 'src/app/core/models/email';

@Component({
  selector: 'app-user-overview',
  templateUrl: './user-overview.component.html',
  styleUrls: ['./user-overview.component.scss']
})
export class UserOverviewComponent implements OnInit {
  @Input()
  members: Member[] = [];

  @Output()
  sendInvitation = new EventEmitter<InviteOutgoingDTO>();

  constructor(private dialog: MatDialog) {}

  ngOnInit() {}

  public inviteUser() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.width = '800px';
    dialogConfig.height = 'fit-content';
    dialogConfig.panelClass = 'dialog-select-icons';

    const dialog = this.dialog.open(UserInviteDialogComponent);
    dialog.afterClosed().subscribe(data => {
      if (data) {
        if (data.groupName !== '') {
          const inviteDate = new InviteOutgoingDTO(data);
          this.sendInvitation.emit(inviteDate);
        }
      }
    });
  }
}
