using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataGrabberConfig.Business.Contract;
using DataGrabberConfig.Services.Contract;
using DataGrabberConfig.Services.ViewModel.Param;
using DataGrabberConfig.Utility.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataGrabberConfig.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private IBusinessAccess _businessAccess { get; }

        private IJwtConfigurator _JwtConfigurator { get; }

        public LoginController(IBusinessAccess businessAccess, IJwtConfigurator jwtConfigurator)
        {
            _businessAccess = businessAccess;
            _JwtConfigurator = jwtConfigurator;
        }


        // GET: api/Login
        [HttpGet]
        [Authorize(Roles = "User")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [Route("verify-user")]
        [HttpPost]
        public ActionResult<IDbResponse> Authenticate([FromBody] LoginViewModelParam user)
        {
            if (user == null || string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                return BadRequest("bad request..");

            var resp = _businessAccess.AuthenticateUser(user);

            if (resp.IsSuccess)
            {
                var tokenString = _JwtConfigurator.GenerateToken(resp);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}