using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoggerApi.Models.Entities
{
    public class Token
    {
        public virtual Guid TokenId { get; set; }
        public virtual string ApplicationId { get; set; }
        public virtual Guid AccessToken { get; set; }
        public virtual DateTime IssuedOn { get; set; }
        public virtual DateTime ExpiresOn { get; set; }
    }
}