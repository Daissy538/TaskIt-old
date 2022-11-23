import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GroupOverviewComponent } from './components/group-overview/group-overview.component';
import { MatIconModule } from '@angular/material/icon';
import { GroupItemComponent } from './components/group-item/group-item.component';
import { CoreModule } from 'src/app/core/core.module';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatDialogModule } from '@angular/material/dialog';
import { GroupUpdateComponent } from './components/group-update/group-update.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { IconSelectorComponent } from 'src/app/core/components/icon-selector/icon-selector.component';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
  imports: [
    MatDialogModule,
    CommonModule,
    MatIconModule,
    DragDropModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    CoreModule,
    MatSelectModule
  ],
  declarations: [GroupOverviewComponent, GroupItemComponent, GroupUpdateComponent, IconSelectorComponent],
  exports: [GroupOverviewComponent, GroupItemComponent, GroupUpdateComponent]
})
export class GroupModule {}
