import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Router } from '@angular/router';
import { Observable, throwError, Subject } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { ApplicationUser, ILoginResponse } from 'src/app/models';
import { LoggerService } from './logger.service';
import { setUserToken, removeUserToken } from 'src/app/functions';
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable({ providedIn: 'root' })
export class LoginService {


  private api = {
    SignUp: 'api/Login/SignUp',
    SignIn: 'api/login/verify-user',
  };

  // Observable string sources
  private loginUserSubject = new Subject<ApplicationUser>();

  // Observable string streams
  loginUser$ = this.loginUserSubject.asObservable();

  constructor(
    private _LoggerService: LoggerService,
    private _http: HttpClient,
    private router: Router,
    private jwtHelper: JwtHelperService) {

  }


  registerUser(appUser: ApplicationUser) {
    this._LoggerService.log('registering user..');
    return this._http.post(this.api.SignUp, appUser);
  }

  SignInUser(appUser: ApplicationUser) {
    this._LoggerService.log('signing in user..');
    return this._http.post(this.api.SignIn, appUser);
  }

  UpdateUserLoggedIn(resp: ILoginResponse) {

    var decodedToken = this.jwtHelper.decodeToken(resp.token);

    let user: ApplicationUser = {};
    user.fullName = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
    user.role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    //console.log(decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid']);
    user.tokenExpiry = decodedToken['exp'];

    this.loginUserSubject.next(user);
    setUserToken(resp.token);
    this.router.navigate(['/']);
  }
  
  Logout() {
    this.loginUserSubject.next(null);
    removeUserToken();
    this.router.navigate(['/login']);
  }

}
