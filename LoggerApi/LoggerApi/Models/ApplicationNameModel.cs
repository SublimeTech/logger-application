using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LoggerApi.Models
{
    [DataContract]
    public class ApplicationNameModel
    {
        [DataMember(Name = "display_name")]
        public string DisplayName { get; set; }
    }
}