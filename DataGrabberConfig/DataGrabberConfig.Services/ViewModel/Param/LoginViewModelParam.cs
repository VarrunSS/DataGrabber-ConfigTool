using DataGrabberConfig.Services.Common;
using DataGrabberConfig.Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataGrabberConfig.Services.ViewModel.Param
{
    public class LoginViewModelParam : ApplicationUser, ILoginUser, IBasicUserInformation
    {
        public LoginViewModelParam() : base()
        {
            LoginUser = string.Empty;
        }

        public string LoginUser { get; set; }

    }
}
