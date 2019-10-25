import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { UserInviteDialogComponent } from '../user-invite-dialog/user-invite-dialog.component';
import { AuthenticationService } from '../../../../pages/authentication/authentication.service';
import { Member } from '../../../../core/models/member';
import { InviteOutgoingDTO } from '../../../../core/models/email';
import { ConfirmationDialogComponent } from 'src/app/core/components/confirmation-dialog/confirmation-dialog.component';

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
  @Output()
  unsubscribe = new EventEmitter<Member>();

  activeUserID: number;

  constructor(
    private dialog: MatDialog,
    private authenticateService: AuthenticationService
  ) {}

  ngOnInit() {
    this.activeUserID = this.authenticateService.getUserID();
  }

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

  public isActiveUser(member: Member) {
    return this.activeUserID === member.userID && this.members.length > 1;
  }

  public unsubscribeUser(member: Member) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {
      autofocus: false,
      description: 'Weet je zeker dat jij je wilt afmelden voor deze group?',
      title: 'Afmelden',
      cancelButtonText: 'Nee',
      confirmButtonText: 'Ja'
    };

    const dialog = this.dialog.open(ConfirmationDialogComponent, dialogConfig);
    dialog.afterClosed().subscribe(data => {
      if (data) {
        this.unsubscribe.emit(member);
      }
    });
  }
}
