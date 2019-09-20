import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DashboardRouting } from './dashboard.routing';
import { GroupOverviewComponent } from '../group/components/group-overview/group-overview.component';
import { CoreModule } from 'src/app/core/core.module';
import { GroupItemComponent } from '../group/components/group-item/group-item.component';

@NgModule({
  imports: [
    CommonModule,
    DashboardRouting,
    CoreModule
  ],
  declarations: [DashboardComponent, GroupOverviewComponent, GroupItemComponent]
})
export class DashboardModule { }
