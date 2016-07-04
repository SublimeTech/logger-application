using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LoggerApi.ActionFilters;
using LoggerApi.Models;
using LoggerApi.Services;

namespace LoggerApi.Controllers
{
    public class LogController : ApiController
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [AuthorizationRequired]
        [Throttle]
        public IHttpActionResult Post([FromBody] LogModel model)
        {
            //TODO: Decorate model with restrinctions
            if (model == null)
            {
                return BadRequest();
            }
            var logId =_logService.Log(model);
            return Ok(new { success = logId != 0 });
        }
    }
}
