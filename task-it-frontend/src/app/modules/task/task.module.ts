import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaksListComponent } from './components/taks-list/taks-list.component';
import { TaskListItemComponent } from './components/task-listItem/task-listItem.component';
import { CoreModule } from 'src/app/core/core.module';

@NgModule({
  imports: [
    CommonModule,
    CoreModule
  ],
  declarations: [TaksListComponent, TaskListItemComponent],
  exports: [TaskListItemComponent]
})
export class TaskModule { }
