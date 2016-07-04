using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoggerApi.Models.Entities
{
    public class SessionConfig
    {
        public virtual int Id { get; set; }
        public virtual int SessionLifeTime { get; set; }
    }
}