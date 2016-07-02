using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoggerApi.Models
{
    public class ApplicationModel
    {
        public string ApplicationId { get; set; }
        public string DisplayName { get; set; }
        public string Secret { get; set; }
    }
}