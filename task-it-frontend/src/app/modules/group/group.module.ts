import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GroupOverviewComponent } from './components/group-overview/group-overview.component';
import { MatIconModule } from '@angular/material/icon';
import { GroupItemComponent } from './components/group-item/group-item.component';
import { CoreModule } from 'src/app/core/core.module';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatDialogModule } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/core/components/confirmation-dialog/confirmation-dialog.component';

@NgModule({
  imports: [
    MatDialogModule,
    CommonModule,
    MatIconModule,
    DragDropModule,
    CoreModule
  ],
  declarations: [GroupOverviewComponent, GroupItemComponent, ConfirmationDialogComponent],
  entryComponents: [ConfirmationDialogComponent]
})
export class GroupModule {}
