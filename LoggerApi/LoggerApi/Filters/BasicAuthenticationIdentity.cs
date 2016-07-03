using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace LoggerApi.Filters
{
    /// <summary>
    /// Basic Authentication identity
    /// </summary>
    public class BasicAuthenticationIdentity : GenericIdentity
    {
        /// <summary>
        /// Get/Set for ApplicationId
        /// </summary>
        public string ApplicationId { get; set; }

        /// <summary>
        /// Get/Set for Secret
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Basic Authentication Identity Constructor
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="secret"></param>
        public BasicAuthenticationIdentity(string applicationId, string secret)
            : base(applicationId, "Basic")
        {
            Secret = secret;
            ApplicationId = applicationId;
        }
    }
}