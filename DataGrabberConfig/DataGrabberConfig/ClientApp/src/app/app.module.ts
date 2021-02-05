import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'


import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { JwtModule } from "@auth0/angular-jwt";
import {
  NgbPaginationModule,
  NgbAlertModule,
  NgbModule
} from '@ng-bootstrap/ng-bootstrap';

import { LoginModule } from './components/login-module/login.module';
import { APIInterceptorProvider, HttpErrorInterceptorProvider, AuthGuard } from './services';
import { BaseUrlProvider, getUserToken } from './functions';
import { AppComponent, AllComponents, CreateUserComponent, ModalPopupInfoComponent, CustomIconComponent } from './components';
import { AllDirectives } from './directives';


@NgModule({
  declarations: [
    AllComponents,
    AllDirectives
  ],
  imports: [
    BrowserAnimationsModule,
    CommonModule,
    BrowserModule,
    LoginModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbPaginationModule,
    NgbAlertModule,
    NgbModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: getUserToken,
        whitelistedDomains: ["localhost:5000"],
        blacklistedRoutes: []
      }
    })
  ],
  providers: [
    BaseUrlProvider,
    APIInterceptorProvider,
    HttpErrorInterceptorProvider,
    AuthGuard
  ],
  entryComponents: [CreateUserComponent, ModalPopupInfoComponent, CustomIconComponent],
  bootstrap: [AppComponent]
})

export class AppModule { }
