import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GroupOverviewComponent } from './components/group-overview/group-overview.component';
import { MatIconModule } from '@angular/material/icon';
import { GroupItemComponent } from './components/group-item/group-item.component';
import {DragDropModule} from '@angular/cdk/drag-drop';
import { CoreModule } from 'src/app/core/core.module';

@NgModule({
  imports: [CommonModule, MatIconModule, DragDropModule, CoreModule],
  declarations: [GroupOverviewComponent, GroupItemComponent]
})
export class GroupModule {}
