import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators, RequiredValidator } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from './auth.service';
import { LogonServiceService } from './logon-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  userAuthenticated: boolean = false;
  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.authService.userAuthenticated$.subscribe((isAuthenticated: boolean) => {
      this.userAuthenticated = isAuthenticated;
    })
  }
  
  logout(){
    this.authService.logout();
  }

}
