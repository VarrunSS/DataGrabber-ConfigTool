import { Component, Input } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';


import { ApplicationUser, IDbResponse } from 'src/app/models';
import { UserService, LoggerService } from 'src/app/services';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})

export class CreateUserComponent {
  @Input() isEditMode: boolean;
  @Input() displayUserGUID: string;

  user: ApplicationUser;
  radioOptions: Array<string>;

  emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
  showMessage: boolean = false;
  alert = {
    message: '',
    type: ''
  };

  constructor(
    public _UserService: UserService,
    private _LoggerService: LoggerService,
    public activeModal: NgbActiveModal) {

    this.user = new ApplicationUser('');
    this.radioOptions = ["Admin", "User"];
  }

  ngOnInit() {

    if (this.displayUserGUID != '') {
      this._UserService.getUser(this.displayUserGUID)
        .subscribe(
          (data: ApplicationUser) => {
            this.user = data;
          },
          error => {
            this.showMessage = true;
            this.alert = { type: 'danger', message: 'Some error occurred!' };
          });
    }
  }

  OnSubmit(form: NgForm) {

    let observable$ = this.isEditMode ? this._UserService.updateUser(form.value) : this._UserService.addUser(form.value),
      successMessage = this.isEditMode ? 'updated' : 'created';

    observable$
      .subscribe(
        (data: IDbResponse) => {
          if (data.isSuccess) {
            this._UserService.getAllUsers();
            this.alert = { type: 'success', message: successMessage + ' successfully!' };
          }
          else {
            this.alert = { type: 'danger', message: data.message };
          }
        },
        error => {
          this.alert = { type: 'danger', message: 'Some error occurred!' };
        })
      .add(() => {
        this.showMessage = true;
      });

  }



  resetForm(form?: NgForm) {
    if (form != null)
      form.reset();
    this.user = {
      userName: '',
      password: '',
      emailAddress: '',
      firstName: '',
      lastName: ''
    }
  }

}
