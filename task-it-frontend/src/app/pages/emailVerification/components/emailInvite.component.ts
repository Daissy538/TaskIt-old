import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EmailVerificationService } from '../emailVerification.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-emailInvite',
  templateUrl: './emailInvite.component.html',
  styleUrls: ['./emailInvite.component.scss']
})
export class EmailInviteComponent implements OnInit {
  subscribingUser: boolean;

  constructor(
    private router: Router,
    private emailService: EmailVerificationService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() {
    const url = this.router.url;
    const urlParts = url.split('/');
    this.subscribeUser(urlParts[urlParts.length - 1]);
    this.subscribingUser = true;
  }

  subscribeUser(token: string) {
    this.emailService.subscribeGroup(token).subscribe(
      response => {
        this.subscribingUser = false;
        this.snackBar.open('Gebruiker is toegevoegd aan groep', 'X', {
          panelClass: ['custom-ok']
        });

        this.router.navigate(['dashboard']);
      },
      error => {
        this.router.navigate(['dashboard']);
      }
    );
  }
}
