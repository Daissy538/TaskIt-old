import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/pages/authentication/authentication.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  private user: string;

  constructor(private authService: AuthenticationService) { }

  ngOnInit() {

  }
}
