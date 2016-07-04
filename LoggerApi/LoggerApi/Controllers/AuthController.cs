using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.ModelBinding;
using LoggerApi.Filters;
using LoggerApi.Models;
using LoggerApi.Models.Entities;
using LoggerApi.Models.Repositories;
using LoggerApi.Services;

namespace LoggerApi.Controllers
{
    public class AuthController : ApiController
    {
        /// <summary>
        /// Services that will encapsule all the token functionality e.g GenerateToken, ValidateToken.
        /// </summary>
        /// <returns></returns>
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// Return a Token to an authenticated client to enable log api calls.
        /// </summary>
        /// <returns></returns>
        [LoggerApiAuthenticationFilter]
        public IHttpActionResult Post()
        {
            if (System.Threading.Thread.CurrentPrincipal != null
                && System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                var identity = System.Threading.Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                if (identity != null)
                {
                    var applicationId = identity.ApplicationId;

                    //Create token
                    var token = _tokenService.GenerateToken(applicationId);
                    return Ok(new { access_token = token.AccessToken.ToString() });
                }
            }
            return BadRequest();
        }
    }
}
