using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Caching;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LoggerApi.ActionFilters
{
    public enum TimeUnit
    {
        Minute = 60,
        Hour = 3600,
        Day = 86400
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ThrottleAttribute : ActionFilterAttribute
    {
//        public TimeUnit TimeUnit { get; set; }
//        public int Count { get; set; }
        private readonly string _message = "Rate limit exceded";
        private readonly string Authorization = "Authorization";

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            if (filterContext.Request.Headers.Contains(Authorization))
            {
                var tokenValue = filterContext.Request.Headers.GetValues(Authorization).First();
                var key = tokenValue;

                if (key != null)
                {
                    // increment the cache value
                    var cnt = 1;
                    if (HttpRuntime.Cache[key] != null)
                    {
                        cnt = (int) HttpRuntime.Cache[key] + 1;
                    }
                    HttpRuntime.Cache.Insert(
                        key,
                        cnt,
                        null,
                        DateTime.UtcNow.AddSeconds(60),
                        Cache.NoSlidingExpiration,
                        CacheItemPriority.Low,
                        null
                        );

                    if (cnt > 60)
                    {
                        var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                        response.Content = new StringContent(_message);
                        filterContext.Response = response;
                    }
                }

            }
            else
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }
    }
}