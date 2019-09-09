import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DashboardRouting } from './dashboard.routing';
import { GroupOverviewComponent } from '../group/group-overview/group-overview.component';

@NgModule({
  imports: [
    CommonModule,
    DashboardRouting,
  ],
  declarations: [DashboardComponent, GroupOverviewComponent]
})
export class DashboardModule { }
