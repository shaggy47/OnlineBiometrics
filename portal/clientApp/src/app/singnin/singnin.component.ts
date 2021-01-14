import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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
    private logonService: AuthService,
    private router: Router
  ) {


  }

  ngOnInit(): void {

  }
  onSubmit() {
    this.logonService.signIn(this.logonFormGroup.controls['userName'].value, this.logonFormGroup.controls['password'].value)
      .subscribe(result => {
        console.log(result);
      });
  }

  register() {
    console.log('clicked');
    this.router.navigateByUrl('/register');
  }


}
