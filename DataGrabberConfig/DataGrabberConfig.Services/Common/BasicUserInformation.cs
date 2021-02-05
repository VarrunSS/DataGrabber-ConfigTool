using DataGrabberConfig.Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataGrabberConfig.Services.Common
{
    public abstract class BasicUserInformation : IBasicUserInformation
    {

        public BasicUserInformation()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            EmailAddress = string.Empty;
        }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string EmailAddress { get; set; }


        public virtual string FullName => string.IsNullOrEmpty(LastName) ? FirstName : FirstName + ' ' + LastName;
    }
}
