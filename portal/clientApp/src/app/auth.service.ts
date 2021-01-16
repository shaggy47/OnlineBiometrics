import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import jwtDecode, { JwtHeader, JwtPayload } from 'jwt-decode';
import { Observable, Subject } from 'rxjs';
import { of } from 'rxjs';



import { environment } from '../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  userAuthenticated$: Subject<boolean>;

  constructor(private httpClient: HttpClient) {
    this.userAuthenticated$ = new Subject<boolean>();
  }

  signIn(userName: string, pass: string): Observable<any> {
    return this.httpClient.post(environment.authUrl + 'signin', { userName: userName, passwordHash: pass });
  }

  setUserInfo(payload: any, token?:any) {

    localStorage.setItem('userName', payload.unique_name);
    localStorage.setItem('role', payload.role);
    localStorage.setItem('token', token);

    if (String(payload.role).toLowerCase().includes('admin'))
      localStorage.setItem('isAdmin', 'true');

    if (String(payload.unique_name) != ''){
      localStorage.setItem('validUser', 'true');
      this.userAuthenticated$.next(true);
    }      
    else
      localStorage.setItem('validUser', 'false');

  }

  isLoggedIn(): boolean {
    return Boolean(localStorage.getItem('validUser'));
  }

  isAdmin() {
    return Boolean(localStorage.getItem('isAdmin'));
  }

  logout(){
    localStorage.clear();
    this.userAuthenticated$.next(false);
  }

  getToken(){
    return localStorage.getItem('token');
  }  

}
