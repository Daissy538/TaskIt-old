import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DashboardRouting } from './dashboard.routing';
import { CoreModule } from '@angular/flex-layout';

@NgModule({
  imports: [
    CommonModule,
    DashboardRouting,
  ],
  declarations: [DashboardComponent]
})
export class DashboardModule { }
