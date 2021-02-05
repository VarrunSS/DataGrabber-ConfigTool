using DataGrabberConfig.Services;
using DataGrabberConfig.Services.Common;
using DataGrabberConfig.Services.Contract;
using DataGrabberConfig.Services.ViewModel;
using DataGrabberConfig.Services.ViewModel.Param;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataGrabberConfig.Business.Contract
{
    public interface IBusinessAccess
    {
        LoginViewModel AuthenticateUser(LoginViewModelParam param);


        List<ApplicationUser> GetAllUsers();

        ApplicationUser GetUser(LoginViewModelParam param);

        IDbResponse AddUser(LoginViewModelParam param);

        IDbResponse UpdateUser(LoginViewModelParam param);

        IDbResponse DeleteUser(LoginViewModelParam param);


        List<BasicConfigurationDetail> GetAllConfigurations();

        ConfigurationViewModel GetConfiguration(ConfigurationViewModelParam param);

        IDbResponse AddConfiguration(ConfigurationViewModelParam param);

        IDbResponse UpdateConfiguration(ConfigurationViewModelParam param);

        IDbResponse DeleteConfiguration(ConfigurationViewModelParam param);

    }
}
