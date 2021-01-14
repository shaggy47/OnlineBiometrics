import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import jwtDecode, { JwtHeader, JwtPayload } from 'jwt-decode';
import { Observable } from 'rxjs';
import { of } from 'rxjs';



import { environment } from '../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient) { }

  signIn(userName: string, pass: string): Observable<boolean> {
    let token: any;
    let isAuthenticated: boolean = false;

    this.httpClient.post(environment.authUrl + 'signin', { userName: userName, passwordHash: pass })
      .subscribe((payload: any) => {

        token = jwtDecode(payload.token, { header: true });
        let decoded = jwtDecode<JwtPayload>(payload.token);
        console.log(decoded);
        
        console.log(token);

        if (token != '')
          isAuthenticated = true;

      });


    return of(isAuthenticated);
  }


}
