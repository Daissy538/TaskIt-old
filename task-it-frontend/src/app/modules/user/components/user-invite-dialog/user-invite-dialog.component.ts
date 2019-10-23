import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormControl, Validators } from '@angular/forms';

export interface InvitationDialogData {
  groupName: string;
}

@Component({
  selector: 'app-user-invite-dialog',
  templateUrl: './user-invite-dialog.component.html',
  styleUrls: ['./user-invite-dialog.component.css']
})
export class UserInviteDialogComponent implements OnInit {
  emailFormControl: FormControl;

  constructor(
    public dialogRef: MatDialogRef<UserInviteDialogComponent>
  ) {}

  ngOnInit() {
    this.emailFormControl = new FormControl('', [
      Validators.required,
      Validators.email
    ]);
  }

  /**
   * Cancel invitation
   */
  public onCancel() {
    this.close('');
  }

  /**
   * Sends invitation
   */
  public onSend() {
    this.close(this.emailFormControl.value);
  }

  private close(email: string) {
    this.dialogRef.close(email);
  }
}
