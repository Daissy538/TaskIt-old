import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface ConfirmationDialogData {
  description: string;
  title: string;
  cancelButtonText: string;
  confirmButtonText: string;
}

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation-dialog.component.html',
  styleUrls: ['./confirmation-dialog.component.scss']
})
export class ConfirmationDialogComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<ConfirmationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ConfirmationDialogData
  ) {}

  ngOnInit() {
    
  }
  
  onCancel(): void {
    this.close(false);
  }

  onAccept(): void {
    this.close(true);
  }

  private close(isConfirmed: boolean) {
    this.dialogRef.close(isConfirmed);
  }
}
