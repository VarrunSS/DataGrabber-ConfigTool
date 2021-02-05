using DataGrabberConfig.Settings;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataGrabberConfig.Utility
{
    public class ConnectionHelper : IConnectionHelper
    {
        private readonly IConfigFields _config;

        public ConnectionHelper(IConfigFields config)
        {
            _config = config;
        }

        public SqlConnection DefaultConnectionDB()
        {
            return new SqlConnection(_config.DefaultConnectionString);
        }

    }

    public interface IConnectionHelper
    {
        SqlConnection DefaultConnectionDB();
    }
}
