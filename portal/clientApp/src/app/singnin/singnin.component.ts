import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LogonServiceService } from '../logon-service.service';

@Component({
  selector: 'app-singnin',
  templateUrl: './singnin.component.html',
  styleUrls: ['./singnin.component.css']
})
export class SingninComponent implements OnInit {

  title = 'Online Kyc Portal';
  logonFormGroup:FormGroup = new FormGroup({
    userName: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
    rememberMe: new FormControl(false)
  });
  /**
   *
   */
  constructor(
      private logonService:LogonServiceService,
      private router:Router
    ) {
    
    
  }

  ngOnInit(): void {
    
  }
  LogIn() {
    console.log(this.logonFormGroup.controls['userName'].value);
    console.log(this.logonFormGroup.controls['password'].value);
  }

  register() {
    console.log('clicked');
    this.router.navigateByUrl('/register');
  }


}
