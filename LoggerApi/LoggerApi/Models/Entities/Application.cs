using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoggerApi.Models.Entities
{
    public class Application
    {
        public virtual string ApplicationId { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Secret { get; set; }
    }
}