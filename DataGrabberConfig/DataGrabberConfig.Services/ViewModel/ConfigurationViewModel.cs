using DataGrabberConfig.Services.Common;
using DataGrabberConfig.Services.Contract;
using DataGrabberConfig.Services.ViewModel.Param;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataGrabberConfig.Services.ViewModel
{
    public class ConfigurationViewModel : ConfigurationViewModelParam, IDbResponse
    {
        public ConfigurationViewModel()
        {
            IsSuccess = false;
            Message = string.Empty;
        }

        public ConfigurationViewModel(ConfigurationViewModelParam param) : this()
        {
            LoginUser = param.LoginUser;
        }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }



    }
}
