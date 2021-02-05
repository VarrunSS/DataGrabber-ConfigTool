using DataGrabberConfig.Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataGrabberConfig.Services.ViewModel.Param
{
    public class ConfigurationViewModelParam : FullConfigurationDetail, ILoginUser
    {

        public ConfigurationViewModelParam() : base()
        {
            LoginUser = string.Empty;
        }

        public string LoginUser { get; set; }


    }
}
