using DataGrabberConfig.Services.Contract;
using DataGrabberConfig.Services.ViewModel.Param;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataGrabberConfig.Services.ViewModel
{
    public class LoginViewModel : LoginViewModelParam, IDbResponse
    {
        public LoginViewModel()
        {
            IsSuccess = false;
            Message = string.Empty;
        }

        public LoginViewModel(LoginViewModelParam param) : this()
        {
            UserName = param.UserName;
            Password = param.Password;
        }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

    }
}
