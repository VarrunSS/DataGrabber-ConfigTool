import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ApplicationUser } from 'src/app/models';
import { LoginService, LoggerService } from 'src/app/services';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})

export class SignUpComponent {

  user: ApplicationUser = new ApplicationUser('');
  emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
  showMessage: boolean = false;
  alert = {
    Message: '',
    type: ''
  };

  constructor(
    private _LoginService: LoginService,
    private _LoggerService: LoggerService) {

  }

  ngOnInit() {
    this.resetForm();
  }


  OnSubmit(form: NgForm) {

    this._LoggerService.log('submitting..')

    this._LoginService.registerUser(form.value)
      .subscribe((data: any) => {
        if (data.Succeeded == true) {
          this.resetForm(form);
          this.showMessage = true;
          //this.toastr.success('User registration successful');
          this.alert = { type: 'success', Message: 'User registration successful' };
        }
        else
          //this.toastr.error(data.Errors[0]);
          this.showMessage = true;
        this.alert = { type: 'danger', Message: 'data.Errors[0]' };

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

