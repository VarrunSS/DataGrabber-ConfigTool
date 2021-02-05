using System;
using System.Collections.Generic;
using System.Text;

namespace DataGrabberConfig.Services.Contract
{
    public interface IDbResponse
    {
        bool IsSuccess { get; set; }

        string Message { get; set; }
    }
}
