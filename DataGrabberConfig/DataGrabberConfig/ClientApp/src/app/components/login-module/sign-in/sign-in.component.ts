import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ApplicationUser, ILoginResponse } from 'src/app/models';
import { LoginService, LoggerService } from 'src/app/services';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})

export class SignInComponent {

  user: ApplicationUser = new ApplicationUser('');
  showMessage: boolean = false;
  alert = {
    message: '',
    type: ''
  };

  constructor(
    //private router: Router,
    private _LoginService: LoginService,
    private _LoggerService: LoggerService) {

  }

  ngOnInit() {
    this.resetForm();
  }



  OnSubmit(form: NgForm) {

    this._LoggerService.log('submitting..')

    this._LoginService.SignInUser(form.value)
      .subscribe(
      (data: ILoginResponse) => {
          this.showMessage = true;
          //this.alert = { type: 'success', message: 'User login successful' };

          // update logged-in user name
          this._LoginService.UpdateUserLoggedIn(data);
          //this.resetForm(form);

        }, error => {
          this.showMessage = true;
          this.alert = { type: 'danger', message: 'Some error occurred!' };

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
