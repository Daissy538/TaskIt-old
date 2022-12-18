import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DashboardRouting } from './dashboard.routing';
import { CoreModule } from 'src/app/core/core.module';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatDialogModule } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/core/components/confirmation-dialog/confirmation-dialog.component';
import { GroupModule } from 'src/app/modules/group/group.module';

@NgModule({
  imports: [
    MatDialogModule,
    GroupModule,
    CommonModule,
    DashboardRouting,
    CoreModule,
    DragDropModule
  ],
  declarations: [DashboardComponent]
})
export class DashboardModule { }
