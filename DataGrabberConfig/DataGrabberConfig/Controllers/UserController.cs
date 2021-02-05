using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataGrabberConfig.Business.Contract;
using DataGrabberConfig.Logger;
using DataGrabberConfig.Services.Common;
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
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {

        private readonly ILogWriter _log;
        private readonly IBusinessAccess _businessAccess;

        public UserController(ILogWriter log, IBusinessAccess businessAccess)
        {
            _log = log;
            _businessAccess = businessAccess;
        }

        // GET: api/User
        [HttpGet]
        [Route("GetAllUsers")]
        public IEnumerable<ApplicationUser> Get()
        {
            var result = new List<ApplicationUser>();
            try
            {
                result = _businessAccess.GetAllUsers();
            }
            catch (Exception ex)
            {
                _log.Write("Exception in UserController; Message: " + ex.Message);
            }
            finally
            {

            }
            return result;
        }

        // GET: api/User/5
        [HttpGet]
        [Route("GetUser/{id}")]
        public ActionResult<ApplicationUser> Get(string id)
        {
            ApplicationUser result = null;

            try
            {
                if (string.IsNullOrEmpty(id))
                    return BadRequest("bad request..");

                var user = new LoginViewModelParam() { DisplayUserGUID = id };
                result = _businessAccess.GetUser(user);

                if (string.IsNullOrEmpty(result.DisplayUserGUID))
                    return NotFound(id);
            }
            catch (Exception ex)
            {
                _log.Write("Exception in UserController; Message: " + ex.Message);
            }
            finally
            {

            }
            return Ok(result);
        }

        // POST: api/User
        [HttpPost]
        [Route("AddUser")]
        public ActionResult<IDbResponse> Post([FromBody] LoginViewModelParam user)
        {
            IDbResponse result = null;

            try
            {
                if (user == null || string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                    return BadRequest("bad request..");

                result = _businessAccess.AddUser(user);
            }
            catch (Exception ex)
            {
                _log.Write("Exception in UserController; Message: " + ex.Message);
            }
            finally
            {

            }
            return Ok(result);
        }

        // PUT: api/User/5
        [HttpPut]
        [Route("UpdateUser/{id}")]
        public ActionResult<IDbResponse> Put(int id, [FromBody] LoginViewModelParam user)
        {
            IDbResponse result = null;

            try
            {
                if (user == null || string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                    return BadRequest("bad request..");

                user.DisplayUserGUID = id.ToString();
                result = _businessAccess.UpdateUser(user);
            }
            catch (Exception ex)
            {
                _log.Write("Exception in UserController; Message: " + ex.Message);
            }
            finally
            {

            }
            return Ok(result);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public ActionResult<IDbResponse> Delete(string id)
        {
            IDbResponse result = null;

            try
            {
                var user = new LoginViewModelParam() { DisplayUserGUID = id };
                result = _businessAccess.DeleteUser(user);
            }
            catch (Exception ex)
            {
                _log.Write("Exception in UserController; Message: " + ex.Message);
            }
            finally
            {

            }
            return Ok(result);
        }


    }
}
