using System;
using System.Collections.Generic;
using System.Text;

namespace DataGrabberConfig.Utility
{
    public class SpHelper
    {
        public const string AuthenticateUser = "dbo.ConfigTool_DataGrabber_Login_Authenticate";

        public const string GetAllUsers = "dbo.ConfigTool_DataGrabber_User_GetAll";
        public const string GetUser = "dbo.ConfigTool_DataGrabber_User_Get";
        public const string AddUser = "dbo.ConfigTool_DataGrabber_User_Add";
        public const string UpdateUser = "dbo.ConfigTool_DataGrabber_User_Update";
        public const string DeleteUser = "dbo.ConfigTool_DataGrabber_User_Delete";

        public const string GetAllConfigurations = "dbo.ConfigTool_DataGrabber_Configuration_GetAll";
        public const string GetConfiguration = "dbo.ConfigTool_DataGrabber_Configuration_Get";
        public const string AddConfiguration = "dbo.ConfigTool_DataGrabber_Configuration_Add";
        public const string UpdateConfiguration = "dbo.ConfigTool_DataGrabber_Configuration_Update";
        public const string DeleteConfiguration = "dbo.ConfigTool_DataGrabber_Configuration_Delete";

    }
}
