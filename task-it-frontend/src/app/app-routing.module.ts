import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () =>
      import('./pages/authentication/authentication.module').then(
        mod => mod.AuthenticationModule
      )
  },
  {
    path: 'dashboard',
    canActivate: [AuthGuard],
    loadChildren: () =>
      import('./pages/dashboard/dashboard.module').then(
        mod => mod.DashboardModule
      )
  },
  {
    path: 'create',
    canActivate: [AuthGuard],
    loadChildren: () =>
      import('./pages/create/create.module').then(
        mod => mod.CreateModule
      )
  },
  {
    path: 'details',
    canActivate: [AuthGuard],
    loadChildren: () =>
    import('./pages/details/details.module').then(
      mod => mod.DetailsModule
    )
  },
  {
    path: 'verification',
    canActivate: [AuthGuard],
    loadChildren: () =>
    import('./pages/emailVerification/emailVerification.module').then(
      mod => mod.EmailVerificationModule
    )
  },
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
