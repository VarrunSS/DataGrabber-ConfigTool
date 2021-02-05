using DataGrabberConfig.Business.Contract;
using DataGrabberConfig.Data.Services.Contract;
using DataGrabberConfig.Logger;
using DataGrabberConfig.Services;
using DataGrabberConfig.Services.Common;
using DataGrabberConfig.Services.Contract;
using DataGrabberConfig.Services.ViewModel;
using DataGrabberConfig.Services.ViewModel.Param;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataGrabberConfig.Business
{
    public class BusinessAccess : IBusinessAccess
    {
        private readonly ILogWriter _log;
        private readonly IDataAccess _dataAccess;

        public BusinessAccess(ILogWriter log, IDataAccess dataAccess)
        {
            _log = log;
            _dataAccess = dataAccess;
        }



        #region Login

        public LoginViewModel AuthenticateUser(LoginViewModelParam param)
        {
            var result = new LoginViewModel();

            try
            {
                result = _dataAccess.AuthenticateUser(param);

            }
            catch (Exception ex)
            {
                _log.Write("Exception in BusinessAccess; Message: " + ex.Message);
            }
            finally
            {

            }
            return result;
        }

        #endregion



        #region Users

        public List<ApplicationUser> GetAllUsers()
        {
            var result = new List<ApplicationUser>();

            try
            {
                result = _dataAccess.GetAllUsers();

            }
            catch (Exception ex)
            {
                _log.Write("Exception in BusinessAccess; Message: " + ex.Message);
            }
            finally
            {

            }
            return result;
        }

        public ApplicationUser GetUser(LoginViewModelParam param)
        {
            var result = new ApplicationUser();

            try
            {
                result = _dataAccess.GetUser(param);

            }
            catch (Exception ex)
            {
                _log.Write("Exception in BusinessAccess; Message: " + ex.Message);
            }
            finally
            {

            }
            return result;
        }
               
        public IDbResponse AddUser(LoginViewModelParam param)
        {
            IDbResponse result = new DbResponse();

            try
            {
                result = _dataAccess.AddUser(param);

            }
            catch (Exception ex)
            {
                _log.Write("Exception in BusinessAccess; Message: " + ex.Message);
            }
            finally
            {

            }
            return result;
        }

        public IDbResponse UpdateUser(LoginViewModelParam param)
        {
            IDbResponse result = new DbResponse();

            try
            {
                result = _dataAccess.UpdateUser(param);

            }
            catch (Exception ex)
            {
                _log.Write("Exception in BusinessAccess; Message: " + ex.Message);
            }
            finally
            {

            }
            return result;
        }

        public IDbResponse DeleteUser(LoginViewModelParam param)
        {
            IDbResponse result = new DbResponse();

            try
            {
                result = _dataAccess.DeleteUser(param);

            }
            catch (Exception ex)
            {
                _log.Write("Exception in BusinessAccess; Message: " + ex.Message);
            }
            finally
            {

            }
            return result;
        }

        #endregion



        #region Configuration

        public List<BasicConfigurationDetail> GetAllConfigurations()
        {
            var result = new List<BasicConfigurationDetail>();

            try
            {
                result = _dataAccess.GetAllConfigurations();

            }
            catch (Exception ex)
            {
                _log.Write("Exception in BusinessAccess; Message: " + ex.Message);
            }
            finally
            {

            }
            return result;
        }

        public ConfigurationViewModel GetConfiguration(ConfigurationViewModelParam param)
        {
            var result = new ConfigurationViewModel();

            try
            {
                result = _dataAccess.GetConfiguration(param);

            }
            catch (Exception ex)
            {
                _log.Write("Exception in BusinessAccess; Message: " + ex.Message);
            }
            finally
            {

            }
            return result;
        }

        public IDbResponse AddConfiguration(ConfigurationViewModelParam param)
        {
            IDbResponse result = new DbResponse();

            try
            {
                param.SetDataBeforeSave();

                result = _dataAccess.AddConfiguration(param);

            }
            catch (Exception ex)
            {
                _log.Write("Exception in BusinessAccess; Message: " + ex.Message);
            }
            finally
            {

            }
            return result;
        }

        public IDbResponse UpdateConfiguration(ConfigurationViewModelParam param)
        {
            IDbResponse result = new DbResponse();

            try
            {
                param.SetDataBeforeSave();

                result = _dataAccess.UpdateConfiguration(param);

            }
            catch (Exception ex)
            {
                _log.Write("Exception in BusinessAccess; Message: " + ex.Message);
            }
            finally
            {

            }
            return result;
        }

        public IDbResponse DeleteConfiguration(ConfigurationViewModelParam param)
        {
            IDbResponse result = new DbResponse();

            try
            {
                result = _dataAccess.DeleteConfiguration(param);

            }
            catch (Exception ex)
            {
                _log.Write("Exception in BusinessAccess; Message: " + ex.Message);
            }
            finally
            {

            }
            return result;
        }

        #endregion

    }
}
