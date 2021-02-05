import { Component, Inject, OnInit } from '@angular/core';
import { DOCUMENT } from "@angular/common";
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

import { UserService } from 'src/app/services';
import { ApplicationUser, IDbResponse, ModalMessage } from 'src/app/models';
import { CreateUserComponent } from '../create-user/create-user.component';
import { ModalPopupInfoComponent } from '../modal-popup-info/modal-popup-info.component';


@Component({
  selector: 'app-manage-users',
  templateUrl: './manage-users.component.html',
  styleUrls: ['./manage-users.component.css']
})

export class ManageUsersComponent implements OnInit {

  users: ApplicationUser[];
  page: number = 1;
  pageSize: number = 5;
  isUnauthorized: boolean = false;
  isLoaded: boolean = false;
  alert: ModalMessage = { message: '', type: '' };

  constructor(
    private _UserService: UserService,
    private modalService: NgbModal) {
    this.users = [];
  }

  ngOnInit() {

    // TODO:v loads users every time from db; must do some client side caching
    this._UserService.getAllUsers();

    this._UserService.allUsers$
      .subscribe((result: ApplicationUser[]) => {
        this.users = result;
        this.isLoaded = true
      },
        error => {
          console.log(error)
          if (error.status === 403) {
            this.isUnauthorized = true;
            this.isLoaded = true;
          }
        });
  }


  open(isEditMode, user) {
    const modalRef = this.modalService.open(CreateUserComponent);
    modalRef.componentInstance.isEditMode = isEditMode;
    modalRef.componentInstance.displayUserGUID = isEditMode ? user.displayUserGUID : '';
  }

  deleteUser(user) {

    this._UserService.deleteUser(user)
      .subscribe(
        (data: IDbResponse) => {
          if (data.isSuccess) {
            this._UserService.getAllUsers();
            this.alert = { title: 'Success', message: 'Deleted Successfully', type: 'success' };
          }
          else {
            this.alert = { title: 'Failed', message: 'Deletion Failed. ' + data.message, type: 'warning' };
          }
        },
        error => {
          this.alert = { title: 'Error', message: 'Some error occured.', type: 'danger' };
        })
      .add(() => {


        const modalRef = this.modalService.open(ModalPopupInfoComponent);
        modalRef.componentInstance.alert = this.alert;
      });
  }

}
