using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using LoggerApi.Services;

namespace LoggerApi.ActionFilters
{
    /// <summary>
    /// Custom Filter that will check for a valid token in the headers. If the token is not valid will send a 403 error.
    /// </summary>
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        private readonly string _authorization = "Authorization";
        private readonly string _message = "Invalid access token";

        /// <summary>
        /// Get the token from the headers and validate it.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            //  Get API key provider
            var tokenService = filterContext.ControllerContext.Configuration
            .DependencyResolver.GetService(typeof(ITokenService)) as ITokenService;

            if (filterContext.Request.Headers.Contains(_authorization))
            {
                var tokenValue = filterContext.Request.Headers.GetValues(_authorization).First();
                Guid token;
                Guid.TryParse(tokenValue, out token);

                if (token == Guid.Empty)
                {
                    InvalidTokenResponse(filterContext);
                }
                else
                {
                    // Validate Token
                    if (tokenService != null && !tokenService.ValidateToken(token))
                    {
                        InvalidTokenResponse(filterContext);
                    }
                }
                
            }
            else
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            base.OnActionExecuting(filterContext);

        }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="filterContext"></param>
        private void InvalidTokenResponse(HttpActionContext filterContext)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            response.Content = new StringContent(_message);
            filterContext.Response = response;
        }
    }
}