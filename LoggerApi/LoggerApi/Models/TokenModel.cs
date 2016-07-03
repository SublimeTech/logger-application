using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoggerApi.Models
{
    public class TokenModel
    {
        public Guid TokenId { get; set; }
        public string ApplicationId { get; set; }
        public Guid AccessToken { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}