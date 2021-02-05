using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataGrabberConfig.Settings
{
    public interface IConfigFields
    {
        string DefaultConnectionString { get; set; }

        AppConfigPathSetting PathSetting { get; set; }

        AppConfigJWTSetting JWTSetting { get; set; }

    }

    public class ConfigFields : IConfigFields
    {
        public ConfigFields()
        {
            DefaultConnectionString = string.Empty;
            PathSetting = new AppConfigPathSetting();
            JWTSetting = new AppConfigJWTSetting();
            Key = string.Empty;
        }

        public ConfigFields(IConfiguration config) : this()
        {
            DefaultConnectionString = config.GetConnectionString("DefaultConnection");
            PathSetting = new AppConfigPathSetting()
            {
                ApplicationPath = config.GetValue<string>("PathSetting:ApplicationPath"),
                LogPath = Path.Combine(config.GetValue<string>("PathSetting:ApplicationPath"),
                                        config.GetValue<string>("PathSetting:LogFolder"))
            };

            config.GetSection("JWTSetting").Bind(JWTSetting);
            Key = config.GetValue<string>("Key");
        }

        public string DefaultConnectionString { get; set; }

        public AppConfigPathSetting PathSetting { get; set; }


        public AppConfigJWTSetting JWTSetting { get; set; }

        public string Key { get; set; }

    }


    public class AppConfigPosition
    {
        public string Title { get; set; }

        public string Name { get; set; }

    }

    public class AppConfigPathSetting
    {
        public string ApplicationPath { get; set; }

        public string LogPath { get; set; }

    }

    public class AppConfigJWTSetting
    {
        public string SecretKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int ExpiresInMinutes { get; set; }


    }



}
