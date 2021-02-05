import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import {
  NgbPaginationModule,
  NgbAlertModule,
  NgbModule
} from '@ng-bootstrap/ng-bootstrap';

import { FormsModule } from '@angular/forms';
import { SignUpComponent } from './sign-up/sign-up.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { UserLoginComponent } from './user-login/user-login.component';
import { LoginRoutingModule } from './login-routing.module';


@NgModule({
  declarations: [
    SignUpComponent,
    SignInComponent,
    UserLoginComponent
  ],
  imports: [
    CommonModule,
    FormsModule,    
    HttpClientModule,
    LoginRoutingModule,
    NgbPaginationModule,
    NgbAlertModule,
    NgbModule
  ],
  exports: [
  ]
})

export class LoginModule {
}
