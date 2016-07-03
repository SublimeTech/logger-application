using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using LoggerApi.Models.Repositories;
using LoggerApi.Services;

namespace LoggerApi.Filters
{
    /// <summary>
    /// Custom Authentication Filter Extending basic Authentication
    /// </summary>
    public class LoggerApiAuthenticationFilter : GenericAuthenticationFilter
    {
        /// <summary>
        /// Default Authentication Constructor
        /// </summary>
        public LoggerApiAuthenticationFilter()
        {
        }

        /// <summary>
        /// Protected overriden method for authorizing user
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="secret"></param>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool OnAuthorizeUser(string applicationId, string secret, HttpActionContext actionContext)
        {
            var appService = actionContext.ControllerContext.Configuration
                               .DependencyResolver.GetService(typeof(IApplicationService)) as IApplicationService;
            if (appService != null)
            {
                applicationId = appService.Authenticate(applicationId, secret);
                if (!string.IsNullOrEmpty(applicationId))
                {
                    var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                    if (basicAuthenticationIdentity != null)
                        basicAuthenticationIdentity.ApplicationId = applicationId;
                    return true;
                }
            }
            return false;
        }
    }
}