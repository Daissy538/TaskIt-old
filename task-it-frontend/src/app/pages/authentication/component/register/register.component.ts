import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  Validators,
  FormGroup,
  FormBuilder
} from '@angular/forms';
import { User } from 'src/app/core/models/user';
import { AuthenticationService } from '../../authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  email: FormControl;
  name: FormControl;
  password: FormControl;
  confirmPassword: FormControl;

  registerForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private authenicationService: AuthenticationService
  ) {}

  ngOnInit() {
    this.email = new FormControl('', [Validators.required, Validators.email]);
    this.name = new FormControl('', [Validators.required]);
    this.password = new FormControl('', [
      Validators.required,
      Validators.pattern(
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/
      )
    ]);
    this.confirmPassword = new FormControl('', [Validators.required]);

    this.registerForm = this.formBuilder.group(
      {
        email: this.email,
        name: this.name,
        password: this.password,
        confirmPassword: this.confirmPassword,
        floatLabel: 'auto'
      },
      { validator: this.passwordMatch() }
    );
  }

  register() {
    const newRegistration = new User(this.email.value, this.name.value, this.password.value);
    this.authenicationService.registerUser(newRegistration);
  }

  passwordMatch(): any {
    return () => {
      const passwordControl = this.password;
      const confirmPasswordControl = this.confirmPassword;

      if (
        confirmPasswordControl.errors &&
        !confirmPasswordControl.errors.mustMatch
      ) {
        // return if another validator has already found an error on the matchingControl
        return;
      }

      // set error on matchingControl if validation fails
      if (passwordControl.value !== confirmPasswordControl.value) {
        confirmPasswordControl.setErrors({ mustMatch: true });
      } else {
        confirmPasswordControl.setErrors(null);
      }
    };
  }

  getErrorMessage(controller: FormControl) {
    return controller.hasError('required')
      ? 'Veld is verplicht'
      : controller.hasError('email')
      ? 'Ongeldige e-mail'
      : controller.hasError('pattern')
      ? 'Ongeldige invoer'
      : controller.hasError('mustMatch')
      ? 'De ingevulde wachtwoorden komen niet overeen'
      : '';
  }
}
