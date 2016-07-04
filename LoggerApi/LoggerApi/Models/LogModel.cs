using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LoggerApi.Models
{
    [DataContract]
    public class LogModel
    {
        public int LogId { get; set; }

        [Required(ErrorMessage = "Logger is required.")]
        [MaxLength(32, ErrorMessage = "Length need to be between 1-32 characters.")]
        [DataMember(Name="logger")]
        public string Logger { get; set; }

        [Required(ErrorMessage = "Level is required.")]
        [MaxLength(256, ErrorMessage = "Length need to be between 1-256 characters.")]
        [DataMember(Name = "level")]
        public string Level { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [MaxLength(2048, ErrorMessage = "Length need to be between 1-2048 characters.")]
        [DataMember(Name = "message")]
        public string Message { get; set; }

        [Required(ErrorMessage = "ApplicationId is required.")]
        [MaxLength(32, ErrorMessage = "Length need to be 32 characters.")]
        [MinLength(32, ErrorMessage = "Length need to be 32 characters.")]
        [DataMember(Name = "application_id")]
        public string ApplicationId { get; set; }
    }
}