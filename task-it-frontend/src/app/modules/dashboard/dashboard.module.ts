import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DashboardRouting } from './dashboard.routing';
import { GroupOverviewComponent } from '../group/components/group-overview/group-overview.component';
import { CoreModule } from 'src/app/core/core.module';
import { GroupItemComponent } from '../group/components/group-item/group-item.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatDialogModule } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/core/components/confirmation-dialog/confirmation-dialog.component';

@NgModule({
  imports: [
    MatDialogModule,
    CommonModule,
    DashboardRouting,
    CoreModule,
    DragDropModule
  ],
  declarations: [DashboardComponent, GroupOverviewComponent, GroupItemComponent, ConfirmationDialogComponent],
  entryComponents: [ConfirmationDialogComponent]
})
export class DashboardModule { }
