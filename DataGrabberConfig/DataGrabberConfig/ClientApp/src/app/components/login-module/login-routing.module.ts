
import { NgModule, ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SignUpComponent } from './sign-up/sign-up.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { UserLoginComponent } from './user-login/user-login.component';

const routes: Routes = [
  {
    path: 'sign-up', component: UserLoginComponent,
    children: [{ path: '', component: SignUpComponent }]
  },
  {
    path: 'login', component: UserLoginComponent,
    children: [{ path: '', component: SignInComponent }]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class LoginRoutingModule { }



//export const LoginRoutingModule: ModuleWithProviders = RouterModule.forChild(routes);
