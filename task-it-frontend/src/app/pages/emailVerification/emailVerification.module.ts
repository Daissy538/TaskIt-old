import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmailInviteComponent } from './components/emailInvite.component';
import { EmailVerificationRoutes } from './emailVerification.routing';
import { CoreModule } from 'src/app/core/core.module';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  imports: [
    CommonModule,
    EmailVerificationRoutes,
    MatProgressSpinnerModule,
    MatIconModule,
    CoreModule
  ],
  declarations: [EmailInviteComponent]
})
export class EmailVerificationModule { }
