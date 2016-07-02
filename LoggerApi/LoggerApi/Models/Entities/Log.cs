using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoggerApi.Models.Entities
{
    public class Log
    {
        public virtual int LogId { get; set; }
        public virtual string Logger { get; set; }
        public virtual string Level { get; set; }
        public virtual string Message { get; set; }
        public virtual string ApplicationId { get; set; }
    }
}