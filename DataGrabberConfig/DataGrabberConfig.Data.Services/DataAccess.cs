using DataGrabberConfig.Data.Services.Contract;
using DataGrabberConfig.Logger;
using DataGrabberConfig.Services;
using DataGrabberConfig.Services.Common;
using DataGrabberConfig.Services.Contract;
using DataGrabberConfig.Services.ViewModel;
using DataGrabberConfig.Services.ViewModel.Param;
using DataGrabberConfig.Settings;
using DataGrabberConfig.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static DataGrabberConfig.Utility.DataHelper;

namespace DataGrabberConfig.Data.Services
{
    public class DataAccess : IDataAccess
    {
        private readonly ILogWriter _log;
        private readonly IConfigFields _config;
        private readonly IConnectionHelper _connectionHelper;

        public DataAccess(ILogWriter log, IConfigFields config, IConnectionHelper connectionHelper)
        {
            _log = log;
            _config = config;
            _connectionHelper = connectionHelper;
        }



        #region Login

        public LoginViewModel AuthenticateUser(LoginViewModelParam param)
        {
            var result = new LoginViewModel(param);

            try
            {
                using (SqlConnection conn = _connectionHelper.DefaultConnectionDB())
                {
                    using (SqlCommand cmd = new SqlCommand(SpHelper.AuthenticateUser, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserName", param.UserName);
                        cmd.Parameters.AddWithValue("@Password", param.Password);

                        using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // get details from reader
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    result.IsSuccess = rdr.CustomGetValue<bool>("IsSuccess");
                                    result.Message = rdr.CustomGetValue<string>("Message");
                                }
                            }

                            if (rdr.NextResult())
                            {
                                while (rdr.Read())
                                {
                                    result.UserID = rdr.CustomGetValue<string>("UserID");
                                    result.UserName = rdr.CustomGetValue<string>("UserName");
                                    result.Password = rdr.CustomGetValue<string>("Password");
                                    result.EmailAddress = rdr.CustomGetValue<string>("Email");
                                    result.FirstName = rdr.CustomGetValue<string>("FirstName");
                                    result.LastName = rdr.CustomGetValue<string>("LastName");
                                    result.IsActive = rdr.CustomGetValue<bool>("IsActive");
                                    result.Role = rdr.CustomGetValue<string>("Role");
                                }
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Write("Exception in DataAccess; Message: " + ex.Message);
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
                using (SqlConnection conn = _connectionHelper.DefaultConnectionDB())
                {
                    using (SqlCommand cmd = new SqlCommand(SpHelper.GetAllUsers, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // get details from reader
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    var user = new ApplicationUser()
                                    {
                                        UserID = rdr.CustomGetValue<string>("UserID"),
                                        DisplayUserGUID = rdr.CustomGetValue<string>("DisplayUserGUID"),
                                        UserName = rdr.CustomGetValue<string>("UserName"),
                                        EmailAddress = rdr.CustomGetValue<string>("Email"),
                                        FirstName = rdr.CustomGetValue<string>("FirstName"),
                                        LastName = rdr.CustomGetValue<string>("LastName"),
                                        IsActive = rdr.CustomGetValue<bool>("IsActive"),
                                        Role = rdr.CustomGetValue<string>("Role")
                                    };
                                    result.Add(user);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Write("Exception in DataAccess; Message: " + ex.Message);
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
                using (SqlConnection conn = _connectionHelper.DefaultConnectionDB())
                {
                    using (SqlCommand cmd = new SqlCommand(SpHelper.GetUser, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DisplayUserGUID", param.DisplayUserGUID);

                        using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // get details from reader
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    result = new ApplicationUser()
                                    {
                                        UserID = rdr.CustomGetValue<string>("UserID"),
                                        DisplayUserGUID = rdr.CustomGetValue<string>("DisplayUserGUID"),
                                        UserName = rdr.CustomGetValue<string>("UserName"),
                                        Password = rdr.CustomGetValue<string>("Password"),
                                        EmailAddress = rdr.CustomGetValue<string>("Email"),
                                        FirstName = rdr.CustomGetValue<string>("FirstName"),
                                        LastName = rdr.CustomGetValue<string>("LastName"),
                                        IsActive = rdr.CustomGetValue<bool>("IsActive"),
                                        Role = rdr.CustomGetValue<string>("Role")
                                    };
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Write("Exception in DataAccess; Message: " + ex.Message);
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
                using (SqlConnection conn = _connectionHelper.DefaultConnectionDB())
                {
                    using (SqlCommand cmd = new SqlCommand(SpHelper.AddUser, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LoginUser", param.LoginUser);
                        cmd.Parameters.AddWithValue("@UserName", param.UserName);
                        cmd.Parameters.AddWithValue("@Password", param.Password);
                        cmd.Parameters.AddWithValue("@Email", param.EmailAddress);
                        cmd.Parameters.AddWithValue("@FirstName", param.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", param.LastName);
                        cmd.Parameters.AddWithValue("@Role", param.Role);

                        using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {

                            // get details from reader
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    result.IsSuccess = rdr.CustomGetValue<bool>("IsSuccess");
                                    result.Message = rdr.CustomGetValue<string>("Message");
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Write("Exception in DataAccess; Message: " + ex.Message);
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
                using (SqlConnection conn = _connectionHelper.DefaultConnectionDB())
                {
                    using (SqlCommand cmd = new SqlCommand(SpHelper.UpdateUser, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LoginUser", param.LoginUser);
                        cmd.Parameters.AddWithValue("@DisplayUserGUID", param.DisplayUserGUID);
                        cmd.Parameters.AddWithValue("@UserName", param.UserName);
                        cmd.Parameters.AddWithValue("@Password", param.Password);
                        cmd.Parameters.AddWithValue("@Email", param.EmailAddress);
                        cmd.Parameters.AddWithValue("@FirstName", param.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", param.LastName);
                        cmd.Parameters.AddWithValue("@Role", param.Role);

                        using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // get details from reader
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    result.IsSuccess = rdr.CustomGetValue<bool>("IsSuccess");
                                    result.Message = rdr.CustomGetValue<string>("Message");
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Write("Exception in DataAccess; Message: " + ex.Message);
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
                using (SqlConnection conn = _connectionHelper.DefaultConnectionDB())
                {
                    using (SqlCommand cmd = new SqlCommand(SpHelper.DeleteUser, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LoginUser", param.LoginUser);
                        cmd.Parameters.AddWithValue("@DisplayUserGUID", param.DisplayUserGUID);

                        using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // get details from reader
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    result.IsSuccess = rdr.CustomGetValue<bool>("IsSuccess");
                                    result.Message = rdr.CustomGetValue<string>("Message");
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Write("Exception in DataAccess; Message: " + ex.Message);
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
                using (SqlConnection conn = _connectionHelper.DefaultConnectionDB())
                {
                    using (SqlCommand cmd = new SqlCommand(SpHelper.GetAllConfigurations, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // get details from reader
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    var user = new BasicConfigurationDetail()
                                    {
                                        ConfigGUID = rdr.CustomGetValue<string>("ConfigGUID"),
                                        ConfigName = rdr.CustomGetValue<string>("ConfigName"),
                                        ConfigType = rdr.CustomGetValue<string>("ConfigType"),
                                        URL = rdr.CustomGetValue<string>("URL"),
                                        CreatedBy = rdr.CustomGetValue<string>("CreatedBy"),
                                        CreatedOn = rdr.CustomGetValue<string>("CreatedOn"),
                                    };
                                    result.Add(user);
                                }
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Write("Exception in DataAccess; Message: " + ex.Message);
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
                using (SqlConnection conn = _connectionHelper.DefaultConnectionDB())
                {
                    using (SqlCommand cmd = new SqlCommand(SpHelper.GetConfiguration, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ConfigGUID", param.ConfigGUID);

                        using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // get details from reader
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    result.IsSuccess = rdr.CustomGetValue<bool>("IsSuccess");
                                    result.Message = rdr.CustomGetValue<string>("Message");
                                }
                            }

                            if (rdr.NextResult())
                            {
                                while (rdr.Read())
                                {
                                    result.ConfigID = rdr.CustomGetValue<string>("ConfigID");
                                    result.ConfigGUID = rdr.CustomGetValue<string>("ConfigGUID");
                                    result.CreatedOn = rdr.CustomGetValue<string>("CreatedOn");
                                    result.CreatedBy = rdr.CustomGetValue<string>("CreatedBy");
                                    result.UpdatedOn = rdr.CustomGetValue<string>("UpdatedOn");
                                    result.UpdatedBy = rdr.CustomGetValue<string>("UpdatedBy");


                                    result.SiteConfiguration = new SiteConfiguration()
                                    {
                                        WebsiteConfigurationName = rdr.CustomGetValue<string>("Configuration_WebsiteConfigurationName"),
                                        ScrapingMechanism = rdr.CustomGetValue<string>("Configuration_ScrapingMechanism"),
                                        ShouldRotateProxy = rdr.CustomGetValue<bool>("Configuration_ShouldRotateProxy"),
                                        RequireInputValues = rdr.CustomGetValue<bool>("Configuration_RequireInputValues"),
                                        ShouldDisableJavaScript = rdr.CustomGetValue<bool>("Configuration_ShouldDisableJavaScript"),
                                        WaitingTimeAfterPageLoad = rdr.CustomGetValue<string>("Configuration_WaitingTimeAfterPageLoad")
                                    };

                                    result.WebsiteDetail = new WebsiteDetail()
                                    {
                                        WebsiteNamePrefix = rdr.CustomGetValue<string>("WebsiteDetail_WebsiteNamePrefix"),
                                        WebscrapeType = rdr.CustomGetValue<string>("WebsiteDetail_WebscrapeType"),
                                        WebsiteURL = rdr.CustomGetValue<string>("WebsiteDetail_WebsiteURL"),
                                        DoesHaveSearchButton = rdr.CustomGetValue<bool>("WebsiteDetail_DoesHaveSearchButton"),
                                        SearchButtonPathType = rdr.CustomGetValue<string>("WebsiteDetail_SearchButtonPathType"),
                                        SearchButton = rdr.CustomGetValue<string>("WebsiteDetail_SearchButton"),
                                        DoesHaveResetButton = rdr.CustomGetValue<bool>("WebsiteDetail_DoesHaveResetButton"),
                                        ResetButtonPathType= rdr.CustomGetValue<string>("WebsiteDetail_ResetButtonPathType"),
                                        ResetButton = rdr.CustomGetValue<string>("WebsiteDetail_ResetButton")
                                    };

                                    result.ProductDetail = new ProductDetail()
                                    {
                                        OverallContainerPathType = rdr.CustomGetValue<string>("ProductDetail_OverallContainerPathType"),
                                        OverallContainer = rdr.CustomGetValue<string>("ProductDetail_OverallContainer"),
                                        ProductContainerPathType = rdr.CustomGetValue<string>("ProductDetail_ProductContainerPathType"),
                                        ProductContainer = rdr.CustomGetValue<string>("ProductDetail_ProductContainer")
                                    };

                                    result.PaginationDetail = new PaginationDetail()
                                    {
                                        PagingType = rdr.CustomGetValue<string>("PaginationDetail_PagingType"),
                                        PaginationContainerPathType = rdr.CustomGetValue<string>("PaginationDetail_PaginationContainerPathType"),
                                        PaginationContainer = rdr.CustomGetValue<string>("PaginationDetail_PaginationContainer"),
                                        DoesHaveNextButton = rdr.CustomGetValue<bool>("PaginationDetail_DoesHaveNextButton"),
                                        NextButtonPathType = rdr.CustomGetValue<string>("PaginationDetail_NextButtonPathType"),
                                        NextButton = rdr.CustomGetValue<string>("PaginationDetail_NextButton"),
                                        ActiveClassForCurrentPage = rdr.CustomGetValue<string>("PaginationDetail_ActiveClassForCurrentPage"),
                                        DisabledClassForLastPage = rdr.CustomGetValue<string>("PaginationDetail_DisabledClassForLastPage"),
                                        ShouldLimitPaging = rdr.CustomGetValue<bool>("PaginationDetail_ShouldLimitPaging"),
                                        PagingLimit = rdr.CustomGetValue<string>("PaginationDetail_PagingLimit")
                                    };

                                    result.MailingInformation = new MailingInformation()
                                    {
                                        ShouldSendMail = rdr.CustomGetValue<bool>("MailingInformation_ShouldSendMail"),
                                        MailToAddress = rdr.CustomGetValue<string>("MailingInformation_MailToAddress"),
                                        MailCCAddress = rdr.CustomGetValue<string>("MailingInformation_MailCCAddress"),
                                        MailBCCAddress = rdr.CustomGetValue<string>("MailingInformation_MailBCCAddress")
                                    };

                                }
                            }

                            if (rdr.NextResult())
                            {
                                while (rdr.Read())
                                {
                                    string url = rdr.CustomGetValue<string>("WebsiteURL");
                                    result.WebsiteDetail.WebsiteURLs.Add(url);
                                }
                            }
                            
                            if (rdr.NextResult())
                            {
                                while (rdr.Read())
                                {
                                    var inputField = new InputField()
                                    {
                                        FieldName = rdr.CustomGetValue<string>("FieldName"),
                                        FieldPathType = rdr.CustomGetValue<string>("FieldPathType"),
                                        FieldPath = rdr.CustomGetValue<string>("FieldPath"),
                                        HasPartnerElement = rdr.CustomGetValue<bool>("HasPartnerElement"),
                                        TargetType = rdr.CustomGetValue<string>("TargetType"),
                                        WaitingTimeAfterElementChange = rdr.CustomGetValue<string>("WaitingTimeAfterElementChange")
                                    };

                                    result.WebsiteDetail.InputFields.Add(inputField);
                                }
                            }

                            if (rdr.NextResult())
                            {
                                while (rdr.Read())
                                {
                                    var field = new Field()
                                    {
                                        FieldName = rdr.CustomGetValue<string>("FieldName"),
                                        FieldPathType = rdr.CustomGetValue<string>("FieldPathType"),
                                        FieldPath = rdr.CustomGetValue<string>("FieldPath"),
                                        ShouldCheckElementInBody = rdr.CustomGetValue<bool>("ShouldCheckElementInBody"),
                                        RemoveText = rdr.CustomGetValue<string>("RemoveText"),
                                        AttributeName = rdr.CustomGetValue<string>("AttributeName")
                                    };

                                    result.ProductDetail.Fields.Add(field);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Write("Exception in DataAccess; Message: " + ex.Message);
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
                using (SqlConnection conn = _connectionHelper.DefaultConnectionDB())
                {
                    using (SqlCommand cmd = new SqlCommand(SpHelper.AddConfiguration, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LoginUser", param.LoginUser);
                        cmd.Parameters.AddWithValue("@WebsiteConfigurationName", param.SiteConfiguration.WebsiteConfigurationName);
                        cmd.Parameters.AddWithValue("@ScrapingMechanism", param.SiteConfiguration.ScrapingMechanism);
                        cmd.Parameters.AddWithValue("@ShouldRotateProxy", param.SiteConfiguration.ShouldRotateProxy);
                        cmd.Parameters.AddWithValue("@RequireInputValues", param.SiteConfiguration.RequireInputValues);
                        cmd.Parameters.AddWithValue("@ShouldDisableJavaScript", param.SiteConfiguration.ShouldDisableJavaScript);
                        cmd.Parameters.AddWithValue("@WaitingTimeAfterPageLoad", param.SiteConfiguration.WaitingTimeAfterPageLoad);
                        cmd.Parameters.AddWithValue("@WebsiteNamePrefix", param.WebsiteDetail.WebsiteNamePrefix);
                        cmd.Parameters.AddWithValue("@WebscrapeType", param.WebsiteDetail.WebscrapeType);
                        cmd.Parameters.AddWithValue("@WebsiteURL", param.WebsiteDetail.WebsiteURL);
                        cmd.Parameters.AddWithValue("@WebsiteURLs", param.WebsiteDetail.DbWebsiteURLs);
                        cmd.Parameters.AddWithValue("@DoesHaveSearchButton", param.WebsiteDetail.DoesHaveSearchButton);
                        cmd.Parameters.AddWithValue("@SearchButtonPathType", param.WebsiteDetail.SearchButtonPathType); 
                        cmd.Parameters.AddWithValue("@SearchButton", param.WebsiteDetail.SearchButton);
                        cmd.Parameters.AddWithValue("@DoesHaveResetButton", param.WebsiteDetail.DoesHaveResetButton);
                        cmd.Parameters.AddWithValue("@ResetButtonPathType", param.WebsiteDetail.ResetButtonPathType);
                        cmd.Parameters.AddWithValue("@ResetButton", param.WebsiteDetail.ResetButton);
                        cmd.Parameters.AddWithValue("@OverallContainerPathType", param.ProductDetail.OverallContainerPathType);
                        cmd.Parameters.AddWithValue("@OverallContainer", param.ProductDetail.OverallContainer);
                        cmd.Parameters.AddWithValue("@ProductContainerPathType", param.ProductDetail.ProductContainerPathType);
                        cmd.Parameters.AddWithValue("@ProductContainer", param.ProductDetail.ProductContainer);
                        cmd.Parameters.AddWithValue("@PagingType", param.PaginationDetail.PagingType);
                        cmd.Parameters.AddWithValue("@PaginationContainerPathType", param.PaginationDetail.PaginationContainerPathType);
                        cmd.Parameters.AddWithValue("@PaginationContainer", param.PaginationDetail.PaginationContainer);
                        cmd.Parameters.AddWithValue("@DoesHaveNextButton", param.PaginationDetail.DoesHaveNextButton);
                        cmd.Parameters.AddWithValue("@NextButtonPathType", param.PaginationDetail.NextButtonPathType);
                        cmd.Parameters.AddWithValue("@NextButton", param.PaginationDetail.NextButton);
                        cmd.Parameters.AddWithValue("@ActiveClassForCurrentPage", param.PaginationDetail.ActiveClassForCurrentPage);
                        cmd.Parameters.AddWithValue("@DisabledClassForLastPage", param.PaginationDetail.DisabledClassForLastPage);
                        cmd.Parameters.AddWithValue("@ShouldLimitPaging", param.PaginationDetail.ShouldLimitPaging);
                        cmd.Parameters.AddWithValue("@PagingLimit", param.PaginationDetail.PagingLimit);
                        cmd.Parameters.AddWithValue("@ShouldSendMail", param.MailingInformation.ShouldSendMail);
                        cmd.Parameters.AddWithValue("@MailToAddress", param.MailingInformation.MailToAddress);
                        cmd.Parameters.AddWithValue("@MailCCAddress", param.MailingInformation.MailCCAddress);
                        cmd.Parameters.AddWithValue("@MailBCCAddress", param.MailingInformation.MailBCCAddress);
                        cmd.Parameters.AddWithValue("@DbInputFieldDetail", param.WebsiteDetail.DbInputFields);
                        cmd.Parameters.AddWithValue("@DbOutputFieldDetail", param.ProductDetail.DbFields);

                        using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {

                            // get details from reader
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    result.IsSuccess = rdr.CustomGetValue<bool>("IsSuccess");
                                    result.Message = rdr.CustomGetValue<string>("Message");
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Write("Exception in DataAccess; Message: " + ex.Message);
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
                using (SqlConnection conn = _connectionHelper.DefaultConnectionDB())
                {
                    using (SqlCommand cmd = new SqlCommand(SpHelper.UpdateConfiguration, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LoginUser", param.LoginUser);
                        cmd.Parameters.AddWithValue("@ConfigGUID", param.ConfigGUID);
                        cmd.Parameters.AddWithValue("@WebsiteConfigurationName", param.SiteConfiguration.WebsiteConfigurationName);
                        cmd.Parameters.AddWithValue("@ScrapingMechanism", param.SiteConfiguration.ScrapingMechanism);
                        cmd.Parameters.AddWithValue("@ShouldRotateProxy", param.SiteConfiguration.ShouldRotateProxy);
                        cmd.Parameters.AddWithValue("@RequireInputValues", param.SiteConfiguration.RequireInputValues);
                        cmd.Parameters.AddWithValue("@ShouldDisableJavaScript", param.SiteConfiguration.ShouldDisableJavaScript);
                        cmd.Parameters.AddWithValue("@WaitingTimeAfterPageLoad", param.SiteConfiguration.WaitingTimeAfterPageLoad);
                        cmd.Parameters.AddWithValue("@WebsiteNamePrefix", param.WebsiteDetail.WebsiteNamePrefix);
                        cmd.Parameters.AddWithValue("@WebscrapeType", param.WebsiteDetail.WebscrapeType);
                        cmd.Parameters.AddWithValue("@WebsiteURL", param.WebsiteDetail.WebsiteURL);
                        cmd.Parameters.AddWithValue("@WebsiteURLs", param.WebsiteDetail.DbWebsiteURLs);
                        cmd.Parameters.AddWithValue("@DoesHaveSearchButton", param.WebsiteDetail.DoesHaveSearchButton);
                        cmd.Parameters.AddWithValue("@SearchButtonPathType", param.WebsiteDetail.SearchButtonPathType);
                        cmd.Parameters.AddWithValue("@SearchButton", param.WebsiteDetail.SearchButton);
                        cmd.Parameters.AddWithValue("@DoesHaveResetButton", param.WebsiteDetail.DoesHaveResetButton);
                        cmd.Parameters.AddWithValue("@ResetButtonPathType", param.WebsiteDetail.ResetButtonPathType);
                        cmd.Parameters.AddWithValue("@ResetButton", param.WebsiteDetail.ResetButton);
                        cmd.Parameters.AddWithValue("@OverallContainerPathType", param.ProductDetail.OverallContainerPathType);
                        cmd.Parameters.AddWithValue("@OverallContainer", param.ProductDetail.OverallContainer);
                        cmd.Parameters.AddWithValue("@ProductContainerPathType", param.ProductDetail.ProductContainerPathType);
                        cmd.Parameters.AddWithValue("@ProductContainer", param.ProductDetail.ProductContainer);
                        cmd.Parameters.AddWithValue("@PagingType", param.PaginationDetail.PagingType);
                        cmd.Parameters.AddWithValue("@PaginationContainerPathType", param.PaginationDetail.PaginationContainerPathType);
                        cmd.Parameters.AddWithValue("@PaginationContainer", param.PaginationDetail.PaginationContainer);
                        cmd.Parameters.AddWithValue("@DoesHaveNextButton", param.PaginationDetail.DoesHaveNextButton);
                        cmd.Parameters.AddWithValue("@NextButtonPathType", param.PaginationDetail.NextButtonPathType);
                        cmd.Parameters.AddWithValue("@NextButton", param.PaginationDetail.NextButton);
                        cmd.Parameters.AddWithValue("@ActiveClassForCurrentPage", param.PaginationDetail.ActiveClassForCurrentPage);
                        cmd.Parameters.AddWithValue("@DisabledClassForLastPage", param.PaginationDetail.DisabledClassForLastPage);
                        cmd.Parameters.AddWithValue("@ShouldLimitPaging", param.PaginationDetail.ShouldLimitPaging);
                        cmd.Parameters.AddWithValue("@PagingLimit", param.PaginationDetail.PagingLimit);
                        cmd.Parameters.AddWithValue("@ShouldSendMail", param.MailingInformation.ShouldSendMail);
                        cmd.Parameters.AddWithValue("@MailToAddress", param.MailingInformation.MailToAddress);
                        cmd.Parameters.AddWithValue("@MailCCAddress", param.MailingInformation.MailCCAddress);
                        cmd.Parameters.AddWithValue("@MailBCCAddress", param.MailingInformation.MailBCCAddress);
                        cmd.Parameters.AddWithValue("@DbInputFieldDetail", param.WebsiteDetail.DbInputFields);
                        cmd.Parameters.AddWithValue("@DbOutputFieldDetail", param.ProductDetail.DbFields);


                        using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // get details from reader
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    result.IsSuccess = rdr.CustomGetValue<bool>("IsSuccess");
                                    result.Message = rdr.CustomGetValue<string>("Message");
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Write("Exception in DataAccess; Message: " + ex.Message);
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
                using (SqlConnection conn = _connectionHelper.DefaultConnectionDB())
                {
                    using (SqlCommand cmd = new SqlCommand(SpHelper.DeleteConfiguration, conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LoginUser", param.LoginUser);
                        cmd.Parameters.AddWithValue("@ConfigGUID", param.ConfigGUID);

                        using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // get details from reader
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    result.IsSuccess = rdr.CustomGetValue<bool>("IsSuccess");
                                    result.Message = rdr.CustomGetValue<string>("Message");
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Write("Exception in DataAccess; Message: " + ex.Message);
            }
            finally
            {

            }
            return result;

        }

        #endregion



    }
}
