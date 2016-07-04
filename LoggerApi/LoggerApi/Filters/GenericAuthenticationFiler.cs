using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace LoggerApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class GenericAuthenticationFilter : AuthorizationFilterAttribute
    {

        /// <summary>
        /// Public default Constructor
        /// </summary>
        public GenericAuthenticationFilter()
        {
        }

        /// <summary>
        /// Checks basic authentication request
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(HttpActionContext filterContext)
        {
            var identity = GetAuthHeader(filterContext);
            if (identity == null)
            {
                SetUnAuthorizedResponse(filterContext);
                return;
            }
            var genericPrincipal = new GenericPrincipal(identity, null);
            Thread.CurrentPrincipal = genericPrincipal;
            if (!OnAuthorizeUser(identity.Name, identity.Secret, filterContext))
            {
                SetUnAuthorizedResponse(filterContext);
                return;
            }
            base.OnAuthorization(filterContext);
        }

        /// <summary>
        /// Virtual method. Can be overriden with the custom Authorization.
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="secret"></param>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        protected virtual bool OnAuthorizeUser(string applicationId, string secret, HttpActionContext filterContext)
        {
            if (string.IsNullOrEmpty(applicationId) || string.IsNullOrEmpty(secret))
                return false;
            return true;
        }

        /// <summary>
        /// Checks for autrhorization header in the request and parses it, creates user credentials and returns as BasicAuthenticationIdentity
        /// </summary>
        /// <param name="filterContext"></param>
        protected virtual BasicAuthenticationIdentity GetAuthHeader(HttpActionContext filterContext)
        {
            string authHeaderValue;
            if (filterContext.Request.Headers.Contains("Authorization"))
            {
                authHeaderValue = filterContext.Request.Headers.GetValues("Authorization").First();
            }
            else
            {
                return null;
            }

            var credentials = authHeaderValue.Split(':');
            return credentials.Length < 2 ? null : new BasicAuthenticationIdentity(credentials[0], credentials[1]);
        }


        /// <summary>
        /// Send the Unauthorized Response
        /// </summary>
        /// <param name="filterContext"></param>
        private static void SetUnAuthorizedResponse(HttpActionContext filterContext)
        {
            filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

    }
}