using DataGrabberConfig.Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataGrabberConfig.Services.Common
{
    public class ApplicationUser : BasicUserInformation, IBasicUserInformation
    {
        public ApplicationUser() : base()
        {
            UserID = string.Empty;
            DisplayUserGUID = string.Empty;
            UserName = string.Empty;
            Password = string.Empty;
            Role = string.Empty;
            IsActive = false;
        }

        public string UserID { get; set; }

        public string DisplayUserGUID { get; set; }
        

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public bool IsActive { get; set; }

    }
}
