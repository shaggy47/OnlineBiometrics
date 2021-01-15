import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import jwtDecode, { JwtPayload } from 'jwt-decode';
import { AuthService } from '../auth.service';
import { LogonServiceService } from '../logon-service.service';

@Component({
  selector: 'app-singnin',
  templateUrl: './singnin.component.html',
  styleUrls: ['./singnin.component.css']
})
export class SingninComponent implements OnInit {

  title = 'Online Kyc Portal';
  logonFormGroup: FormGroup = new FormGroup({
    userName: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
    rememberMe: new FormControl(false)
  });
  /**
   *
   */
  constructor(
    private authService: AuthService,
    private router: Router
  ) {


  }

  ngOnInit(): void {

  }
  onSubmit() {
    let token: any;
    let isAuthenticated: boolean = false;
    this.authService.signIn(this.logonFormGroup.controls['userName'].value, this.logonFormGroup.controls['password'].value)
      .subscribe((payload: any) => {

        token = jwtDecode(payload.token, { header: true });
        let decoded = jwtDecode<JwtPayload>(payload.token);
        this.authService.setUserInfo(decoded);

        if (token != '')
          isAuthenticated = true;

        if (this.authService.isAdmin())
          this.router.navigateByUrl('/review');
        else
          this.router.navigateByUrl('/upload');

      });
  }

  register() {
    console.log('clicked');
    this.router.navigateByUrl('/register');
  }


}
