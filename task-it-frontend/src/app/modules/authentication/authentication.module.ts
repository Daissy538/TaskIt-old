import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './component/register/register.component';
import { LoginComponent } from './component/login/login.component';
import { AuthenticationRouting } from './authentication.routing';

@NgModule({
  imports: [CommonModule, AuthenticationRouting],
  declarations: [LoginComponent, RegisterComponent]
})
export class AuthenticationModule {}
