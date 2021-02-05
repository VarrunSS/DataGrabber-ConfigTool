using DataGrabberConfig.Services.Common;
using DataGrabberConfig.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataGrabberConfig.Utility.Extensions
{
    public class JwtConfigurator : IJwtConfigurator
    {

        private IConfigFields _config;

        public JwtConfigurator(IConfigFields config)
        {
            _config = config;
        }

        public string GenerateToken(ApplicationUser user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.JWTSetting.SecretKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Sid, user.UserID)
                };

            var tokeOptions = new JwtSecurityToken(
                issuer: _config.JWTSetting.Issuer,
                audience: _config.JWTSetting.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_config.JWTSetting.ExpiresInMinutes),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }



    }

    public interface IJwtConfigurator
    {
        string GenerateToken(ApplicationUser user);
    }
}
