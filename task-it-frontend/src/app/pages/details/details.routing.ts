import { Routes, RouterModule } from '@angular/router';
import { GroupDetailsComponent } from './components/group-details/group-details.component';
import { NgModule } from '@angular/core';

const routes: Routes = [
  { path: 'group/:id', component: GroupDetailsComponent },
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class DetailsRouting {}
