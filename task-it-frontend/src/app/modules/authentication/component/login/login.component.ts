import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { AuthenticationService } from '../../authentication.service';
import { User } from 'src/app/core/models/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  email: FormControl;
  password: FormControl;
  loginForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private authenicationService: AuthenticationService
  ) {}

  ngOnInit() {
    this.email = new FormControl('', [Validators.required, Validators.email]);
    this.password = new FormControl('', [Validators.required]);

    this.loginForm = this.formBuilder.group({
      email: this.email,
      password: this.password,
      floatLabel: 'auto'
    });
  }

  /**
   * Login the user with the filled in credentials of the form
   */
  login() {
    const newLoginUser = new User(this.email.value, '', this.password.value);
    this.authenicationService.loginUser(newLoginUser);
  }

  /**
   * Gets error message for the invalid form fields
   * @param controller The form field where an error occured
   */
  getErrorMessage(controller: FormControl) {
    return controller.hasError('required')
    ? 'Veld is verplicht'
    : controller.hasError('email')
    ? 'Ongeldige e-mail'
    : '';
  }
}
