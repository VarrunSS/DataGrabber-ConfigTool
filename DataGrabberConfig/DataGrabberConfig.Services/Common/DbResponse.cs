using DataGrabberConfig.Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataGrabberConfig.Services.Common
{
    public class DbResponse : IDbResponse
    {
        public DbResponse()
        {
            this.IsSuccess = true;
            this.Message = string.Empty;
        }

        public DbResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public DbResponse(string message)
        {
            Message = message;
        }

        public DbResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public string LogIsSuccess => IsSuccess ? "SUCCESS" : "FAILED";

    }
}