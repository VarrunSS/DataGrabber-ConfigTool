
import { NgModule, ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { } from '@angular/core';
import {
  ManageConfigComponent,
  ManageUsersComponent,
  CreateUserComponent,
  CreateConfigurationComponent
} from './components';
import { LoginModule } from './components/login-module/login.module';
import { AuthGuard } from './services';

const routes: Routes = [
  //{
  //  path: 'user',
  //  loadChildren: () => import('./components/login-module/login.module').then(v => v.LoginModule)
  //},
  { path: 'manage-config', component: ManageConfigComponent },
  { path: 'manage-users', component: ManageUsersComponent, canActivate: [AuthGuard] },
  { path: 'create-config', component: CreateConfigurationComponent },
  { path: 'edit-config', component: CreateConfigurationComponent },
  { path: '', redirectTo: '/manage-config', pathMatch: 'full' },
  { path: '**', component: ManageUsersComponent },
  //{ path: '**', component: PageNotFoundComponent },  // Wildcard route for a 404 page
];

//export const AppRoutingModule: ModuleWithProviders = RouterModule.forRoot(routes);


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})


export class AppRoutingModule { }
