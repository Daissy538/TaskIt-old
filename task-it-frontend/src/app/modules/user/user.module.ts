import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserOverviewComponent } from './components/user-overview/user-overview.component';
import { MatIconModule } from '@angular/material/icon';
import {MatListModule} from '@angular/material/list';
import { CoreModule } from 'src/app/core/core.module';
import { UserInviteDialogComponent } from './components/user-invite-dialog/user-invite-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@NgModule({
  imports: [
    CommonModule,
    MatIconModule,
    MatListModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    CoreModule
  ],
  declarations: [UserOverviewComponent, UserInviteDialogComponent],
  exports: [UserOverviewComponent]
})
export class UserModule { }
