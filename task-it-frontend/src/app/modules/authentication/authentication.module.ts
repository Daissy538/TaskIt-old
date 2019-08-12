import { NgModule } from '@angular/core';
import { RegisterComponent } from './component/register/register.component';
import { LoginComponent } from './component/login/login.component';
import { AuthenticationRouting } from './authentication.routing';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CoreModule } from 'src/app/core/core.module';

@NgModule({
  imports: [
    AuthenticationRouting,
    MatFormFieldModule,
    MatInputModule,
    CoreModule
  ],
  declarations: [LoginComponent, RegisterComponent]
})
export class AuthenticationModule {}
