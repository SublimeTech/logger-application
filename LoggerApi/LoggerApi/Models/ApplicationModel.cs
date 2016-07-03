using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LoggerApi.Models
{
    [DataContract]
    public class ApplicationModel
    {
        [DataMember(Name = "application_id")]
        public string ApplicationId { get; set; }

        [DataMember(Name = "display_name")]
        public string DisplayName { get; set; }

        [DataMember(Name = "application_secret")]
        public string Secret { get; set; }
    }
}