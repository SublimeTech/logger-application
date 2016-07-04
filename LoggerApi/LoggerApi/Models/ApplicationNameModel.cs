using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LoggerApi.Models
{
    [DataContract]
    public class ApplicationNameModel
    {
        [DataMember(Name = "display_name")]
        [Required(ErrorMessage= "Display Name is required.")]
        [MaxLength(32, ErrorMessage= "Length need to be between 1-32 characters.")]
        public string DisplayName { get; set; }
    }
}