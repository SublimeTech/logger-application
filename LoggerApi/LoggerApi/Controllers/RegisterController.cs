using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using LoggerApi.Models.Entities;
using LoggerApi.Models.Repositories;
using LoggerApi.Services;
using MlkPwgen;

namespace LoggerApi.Controllers
{
    public class RegisterController : ApiController
    {
        private IApplicationService _appService;

        public RegisterController(IApplicationService appService)
        {
            _appService = appService;
        }

        public IHttpActionResult Get(int id)
        {
            return Ok("Success");
        }

        public IHttpActionResult Post([FromBody] string displayName)
        {
            var application = _appService.CreateNewApplication(displayName);
            if (application == null)
            {
                return BadRequest();
            }
            return Ok(application);
        }
    }
}
