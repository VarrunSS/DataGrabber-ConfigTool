using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataGrabberConfig.CustomAttributes
{
    public class CustomAuthAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>  
        /// This will Authorize User  
        /// </summary>  
        /// <returns></returns>  
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            // TODO:
            //string jwt = filterContext.HttpContext.GetTokenAsync("access_token")
            //    .ConfigureAwait(true).GetAwaiter().GetResult();


            //var handler = new JwtSecurityTokenHandler();
            //var token = handler.ReadJwtToken(jwt);


        }


    }
}
