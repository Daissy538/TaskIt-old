import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/pages/authentication/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  constructor(
    private authService: AuthenticationService,
    private router: Router
  ) {}

  ngOnInit() {}

  canLogout(): boolean {
    return this.authService.isLoggedIn();
  }

  logout() {
    this.authService.logOut();
  }

  navigate() {
    this.router.navigate(['/dashboard']);
  }
}
