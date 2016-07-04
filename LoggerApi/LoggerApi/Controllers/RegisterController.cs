using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using LoggerApi.Models;
using LoggerApi.Models.Entities;
using LoggerApi.Models.Repositories;
using LoggerApi.Services;
using MlkPwgen;

namespace LoggerApi.Controllers
{
    public class RegisterController : ApiController
    {
        /// <summary>
        /// Service that will encapsule all the application functionality e.g Create a new application.
        /// </summary>
        /// <returns></returns>
        private readonly IApplicationService _appService;

        public RegisterController(IApplicationService appService)
        {
            _appService = appService;
        }

        public IHttpActionResult Get(int id)
        {
            return Ok("Success");
        }

        /// <summary>
        /// Use application service to create a new application and returns an application json object
        /// </summary>
        /// <param name="model">A model that contains the display name of the application.</param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] ApplicationNameModel model)
        {
            if (ModelState.IsValid)
            {
                var application = _appService.CreateNewApplication(model.DisplayName);
                if (application == null)
                {
                    return BadRequest();
                }
                return Ok(application);
            }
            return BadRequest(ModelState);
        }
    }
}
