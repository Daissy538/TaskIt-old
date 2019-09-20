import { Routes, RouterModule } from '@angular/router';
import { CreateGroupComponent } from './components/create-group/create-group.component';
import { NgModule } from '@angular/core';

const routes: Routes = [
  { path: 'group', component: CreateGroupComponent },
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class CreateStepsRouting {}
