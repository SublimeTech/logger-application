using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoggerApi.Models.Entities;

namespace LoggerApi.Models.Mappers
{
    public class ApplicationMapper : IApplicationMapper
    {
        public Application MappTo(ApplicationModel model)
        {
            return new Application()
            {
                ApplicationId = model.ApplicationId,
                DisplayName = model.DisplayName,
                Secret = model.Secret
            };
        }

        public ApplicationModel MappTo(Application model)
        {
            return new ApplicationModel()
            {
                ApplicationId = model.ApplicationId,
                DisplayName = model.DisplayName,
                Secret = model.Secret
            };
        }
    }
}