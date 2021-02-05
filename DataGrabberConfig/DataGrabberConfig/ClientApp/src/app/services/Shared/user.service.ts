import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, throwError, Subject, ReplaySubject } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { LoggerService } from './logger.service';
import { ApplicationUser, IDbResponse } from 'src/app/models';

@Injectable({ providedIn: 'root' })
export class UserService {

  api = {
    GetAllUsers: 'api/User/GetAllUsers/',
    GetUser: 'api/User/GetUser/',
    AddUser: 'api/User/AddUser/',
    UpdateUser: 'api/User/UpdateUser/',
    DeleteUser: 'api/User/DeleteUser/'
  };

  constructor(
    private _LoggerService: LoggerService,
    private _http: HttpClient
  ) {

  }

  users: ApplicationUser[] = [];

  // Observable string sources
  private allUsersSubject = new Subject<ApplicationUser[]>();

  // Observable string streams
  allUsers$ = this.allUsersSubject.asObservable();

  getAllUsers() {
    this._LoggerService.log('getting users..');
    this._http.get<ApplicationUser[]>(this.api.GetAllUsers)
        .subscribe((res: ApplicationUser[]) => {
        this.allUsersSubject.next(res);
      });
  }

  getUser(displayUserGUID: string): Observable<ApplicationUser> {
    this._LoggerService.log(`getting user details of ${displayUserGUID}..`);
    return this._http.get<ApplicationUser>(this.api.GetUser + displayUserGUID);
  }

  addUser(user: ApplicationUser) {
    this._LoggerService.log('creating user..');
    return this._http.post<IDbResponse>(this.api.AddUser, user);
  }

  updateUser(user: ApplicationUser) {
    this._LoggerService.log('updating user..');
    return this._http.put<IDbResponse>(this.api.UpdateUser + user.displayUserGUID, user);
  }

  deleteUser(user: ApplicationUser) {
    this._LoggerService.log('deleting user..');
    return this._http.delete<IDbResponse>(this.api.DeleteUser + user.displayUserGUID);
  }




}
