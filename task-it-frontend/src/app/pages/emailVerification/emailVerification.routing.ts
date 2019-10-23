import { Routes, RouterModule } from '@angular/router';
import { EmailInviteComponent } from './components/emailInvite.component';
import { NgModule } from '@angular/core';

const routes: Routes = [
  {
    path: 'invite/:string',
    component: EmailInviteComponent
  },
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class EmailVerificationRoutes {}
