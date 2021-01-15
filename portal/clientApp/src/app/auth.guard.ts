import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router, RouterState } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { SingninComponent } from './singnin/singnin.component';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService,
    private router: Router) {

  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if (route.url[0].toString() == 'review' && this.authService.isLoggedIn() && this.authService.isAdmin()) {
      return true;
    } else if (route.url[0].toString() == 'upload' && this.authService.isLoggedIn() && !this.authService.isAdmin()) {
      return true;
    } else if (!this.authService.isLoggedIn()) {
      this.router.navigateByUrl('sign-in');
    }
    else
      return false;

  }

  // private handleAuthorization(state: RouterStateSnapshot, route: ActivatedRouteSnapshot) {
  //   if (!this.authService.isLoggedIn()) {
  //     this.router.navigateByUrl('sign-in');
  //   } else {
  //     if (route.url[0].toString() == 'review' && this.authService.isAdmin())
  //       this.router.navigateByUrl('review');
  //     else if (this.authService.isLoggedIn())
  //       this.router.navigateByUrl('upload');

  //   }
  // }

}
