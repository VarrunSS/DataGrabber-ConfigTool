using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder.Internal;
using DataGrabberConfig.Logger;
using DataGrabberConfig.Settings;
using DataGrabberConfig.Data.Services;
using DataGrabberConfig.Business;
using DataGrabberConfig.Data.Services.Contract;
using DataGrabberConfig.Business.Contract;

namespace DataGrabberConfig.Utility.Extensions
{
    public static class ServiceExtensions
    {

        public static void Inject(this IServiceCollection services, IConfiguration Configuration)
        {
            // OPTION I
            // Add the whole configuration object here.
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IConfigFields, ConfigFields>();
            services.AddTransient<IConnectionHelper, ConnectionHelper>();
            services.AddTransient<IJwtConfigurator, JwtConfigurator>();
            services.AddTransient<ILogWriter, LogWriter>();

            services.AddTransient<IDataAccess, DataAccess>();
            services.AddTransient<IBusinessAccess, BusinessAccess>();
            services.AddTransient<ICryptoHelper, CryptoHelper>();

            // OPTION II
            //OptionsConfigurationServiceCollectionExtensions.Configure<AppSettingModel>(services, Configuration.GetSection("Position"));

        }

        public static void InitializeJWT(this IServiceCollection services, IConfiguration Configuration)
        {
            var JWTSetting = new AppConfigJWTSetting();
            Configuration.GetSection("JWTSetting").Bind(JWTSetting);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = JWTSetting.Issuer,
                    ValidAudience = JWTSetting.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSetting.SecretKey))
                };
            });

        }



        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

       

    }
}
