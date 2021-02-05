using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataGrabberConfig.Business.Contract;
using DataGrabberConfig.CustomAttributes;
using DataGrabberConfig.Logger;
using DataGrabberConfig.Services;
using DataGrabberConfig.Services.Contract;
using DataGrabberConfig.Services.ViewModel;
using DataGrabberConfig.Services.ViewModel.Param;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataGrabberConfig.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [CustomAuth]
    public class ConfigurationController : ControllerBase
    {

        private readonly ILogWriter _log;
        private readonly IBusinessAccess _businessAccess;

        public ConfigurationController(ILogWriter log, IBusinessAccess businessAccess)
        {
            _log = log;
            _businessAccess = businessAccess;
        }


        [HttpGet]
        [Route("GetAllConfigurations")]
        public IEnumerable<BasicConfigurationDetail> Get()
        {
            var result = new List<BasicConfigurationDetail>();
            try
            {
                result = _businessAccess.GetAllConfigurations();
            }
            catch (Exception ex)
            {
                _log.Write("Exception in ConfigurationController; Message: " + ex.Message);
            }
            finally
            {

            }
            return result;
        }

        // GET: api/User/5
        [HttpGet]
        [Route("GetConfiguration/{id}")]
        public ActionResult<ConfigurationViewModel> Get(string id)
        {
            ConfigurationViewModel result = null;

            try
            {
                if (string.IsNullOrEmpty(id))
                    return BadRequest("bad request..");

                var config = new ConfigurationViewModelParam() { ConfigGUID = id };
                result = _businessAccess.GetConfiguration(config);

                if (string.IsNullOrEmpty(result.SiteConfiguration.WebsiteConfigurationName))
                    return NotFound(id);
            }
            catch (Exception ex)
            {
                _log.Write("Exception in ConfigurationController; Message: " + ex.Message);
            }
            finally
            {

            }
            return Ok(result);
        }

        // POST: api/User
        [HttpPost]
        [Route("AddConfiguration")]
        public ActionResult<IDbResponse> Post([FromBody] ConfigurationViewModelParam config)
        {
            IDbResponse result = null;

            try
            {
                if (config == null || string.IsNullOrEmpty(config.SiteConfiguration.WebsiteConfigurationName))
                    return BadRequest("bad request..");

                result = _businessAccess.AddConfiguration(config);
            }
            catch (Exception ex)
            {
                _log.Write("Exception in ConfigurationController; Message: " + ex.Message);
            }
            finally
            {

            }
            return Ok(result);
        }

        // PUT: api/User/5
        [HttpPut]
        [Route("UpdateConfiguration/{id}")]
        public ActionResult<IDbResponse> Put(string id, [FromBody] ConfigurationViewModelParam config)
        {
            IDbResponse result = null;

            try
            {
                if (config == null || string.IsNullOrEmpty(id))
                    return BadRequest("bad request..");

                config.ConfigGUID = id;
                result = _businessAccess.UpdateConfiguration(config);
            }
            catch (Exception ex)
            {
                _log.Write("Exception in ConfigurationController; Message: " + ex.Message);
            }
            finally
            {

            }
            return Ok(result);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        [Route("DeleteConfiguration/{id}")]
        public ActionResult<IDbResponse> Delete(string id)
        {
            IDbResponse result = null;

            try
            {
                var user = new ConfigurationViewModelParam() { ConfigGUID = id };
                result = _businessAccess.DeleteConfiguration(user);
            }
            catch (Exception ex)
            {
                _log.Write("Exception in ConfigurationController; Message: " + ex.Message);
            }
            finally
            {

            }
            return Ok(result);
        }


    }
}