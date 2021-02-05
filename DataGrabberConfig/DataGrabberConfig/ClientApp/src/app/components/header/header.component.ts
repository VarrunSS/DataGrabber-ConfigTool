import { Component, OnInit } from '@angular/core';
import { LoginService, UserService } from 'src/app/services';
import { JwtHelperService } from '@auth0/angular-jwt';
import { getUserToken } from 'src/app/functions';
import { ILoginResponse, ApplicationUser } from 'src/app/models';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})

export class HeaderComponent implements OnInit {

  loginUser: string;
  isAdmin: boolean;

  constructor(
    private _LoginService: LoginService,
    private _UserService: UserService,
    private jwtHelper: JwtHelperService) {
    this.isAdmin = false;
  }

  ngOnInit() {


    this._LoginService.loginUser$.subscribe(
      (user: ApplicationUser) => {
        if (user != null) {
          this.loginUser = user.fullName;
          this.isAdmin = (user.role == 'Admin');

        }
      });

    this.isUserAuthenticated();
  }

  isUserAuthenticated() {
    const token: string = getUserToken();
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      var resp: ILoginResponse = { token: token };
      this._LoginService.UpdateUserLoggedIn(resp);
      return true;
    }
    else {
      return false;
    }
  }


  Logout() {
    this._LoginService.Logout();
  }
}
