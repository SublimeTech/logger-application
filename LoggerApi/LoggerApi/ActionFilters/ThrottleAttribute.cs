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
    /// <summary>
    /// Custom Filter that will limit the rate of api calls to a given number in a given time.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ThrottleAttribute : ActionFilterAttribute
    {
        private readonly string _message = "Application Rate Limit Exceeded";
        private readonly string _authorization = "Authorization";
        private string _rateLimitedKey = string.Empty;
        private readonly int MINUTE = 60;
        private readonly int FIVE_MINUTES = 300;
        private readonly int API_CALLS_ALLOWED = 60;

        /// <summary>
        /// Limit the API calls for an application to 60 in 1 minute.
        /// If the limit is reached, will avoid any call for the next five minutes.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            if (filterContext.Request.Headers.Contains(_authorization))
            {
                var tokenValue = filterContext.Request.Headers.GetValues(_authorization).First();
                var key = tokenValue;
                if (key != null)
                {
                    // increment the cache value
                    var count = 1;

                    if (HttpRuntime.Cache[_rateLimitedKey] != null)
                    {
                        count = (int) HttpRuntime.Cache[_rateLimitedKey] + 1;
                    }
                    else
                    {
                        if (HttpRuntime.Cache[key] != null)
                        {
                            count = (int)HttpRuntime.Cache[key] + 1;
                        }

                        //Create Cache entry that expires in one MINUTE
                        CreateCacheEntry(key, count, MINUTE);
                    }


                    if (count > API_CALLS_ALLOWED)
                    {
                        var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                        response.Content = new StringContent(_message);
                        filterContext.Response = response;

                        //Set another cache entry to wait 5 minutes before be allowed to make api calls again.
                        if (string.IsNullOrEmpty(_rateLimitedKey))
                        {
                            _rateLimitedKey = string.Format("{0}{1}", key, Guid.NewGuid().ToString());
                        }
                        CreateCacheEntry(_rateLimitedKey, count, FIVE_MINUTES);
                    }
                }

            }
            else
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }

        /// <summary>
        /// Creates a Cache entry that will be used for the rate limit logic.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <param name="seconds"></param>
        private void CreateCacheEntry(string key, int count, int seconds)
        {
            HttpRuntime.Cache.Insert(key, count, null, DateTime.UtcNow.AddSeconds(seconds), Cache.NoSlidingExpiration, CacheItemPriority.Low, null);
        }
    }
}