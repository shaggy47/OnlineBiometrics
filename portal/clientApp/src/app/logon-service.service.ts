import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import {map } from 'rxjs/operators';
import { UserModel } from './models/user-model';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LogonServiceService {
  
  loggedIn:Subject<boolean>;
  baseUrl:string;

  constructor(private http:HttpClient) { 
    
    this.baseUrl = environment.baseUrl;    
  }

  login(userModel: UserModel): Observable<any> {
    return this.http.post(this.baseUrl, userModel);
  }
}
