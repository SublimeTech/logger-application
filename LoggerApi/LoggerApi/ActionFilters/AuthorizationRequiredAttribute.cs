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
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        private readonly string _authorization = "Authorization";
        private readonly string _message = "Invalid access token";

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

        private void InvalidTokenResponse(HttpActionContext filterContext)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            response.Content = new StringContent(_message);
            filterContext.Response = response;
        }
    }
}