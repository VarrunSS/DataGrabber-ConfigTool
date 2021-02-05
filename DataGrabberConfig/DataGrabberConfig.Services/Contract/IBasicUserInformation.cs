using System;
using System.Collections.Generic;
using System.Text;

namespace DataGrabberConfig.Services.Contract
{
    public interface IBasicUserInformation
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string EmailAddress { get; set; }

        string FullName { get; }

    }
}
