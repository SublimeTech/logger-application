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
        /// <summary>
        /// Service that will encapsule all the log functionality e.g Log a message.
        /// </summary>
        /// <returns></returns>
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        /// <summary>
        /// Logs a message in the DB.
        /// </summary>
        /// <param name="model">The log model that is received from the client.</param>
        /// <returns></returns>
        [AuthorizationRequired]
        [Throttle]
        public IHttpActionResult Post([FromBody] LogModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var logId =_logService.Log(model);
            return Ok(new { success = logId != 0 });
        }
    }
}
