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
        private const string Authorization = "Authorization";

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            //  Get API key provider
            var tokenService = filterContext.ControllerContext.Configuration
            .DependencyResolver.GetService(typeof(ITokenService)) as ITokenService;

            if (filterContext.Request.Headers.Contains(Authorization))
            {
                var tokenValue = filterContext.Request.Headers.GetValues(Authorization).First();
                Guid token;
                Guid.TryParse(tokenValue, out token);

                if (token == Guid.Empty)
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    filterContext.Response = responseMessage;
                }
                else
                {
                    // Validate Token
                    if (tokenService != null && !tokenService.ValidateToken(token))
                    {
                        var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        filterContext.Response = responseMessage;
                    }
                }
                
            }
            else
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            base.OnActionExecuting(filterContext);

        }
    }
}