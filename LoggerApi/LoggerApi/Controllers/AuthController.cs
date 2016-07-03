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
        private readonly IRepository _repository;
        private readonly ITokenService _tokenService;

        public AuthController(IRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

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
