using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoggerApi.Models;
using MlkPwgen;

namespace LoggerApi.Services
{
    public class ApplicationService : IApplicationService
    {
        public ApplicationModel CreateNewApplication(string displayName)
        {
            //TODO: Create an application and save it in the DB.
            return new ApplicationModel()
            {
                DisplayName = displayName,
                ApplicationId = PasswordGenerator.Generate(32),
                Secret = PasswordGenerator.Generate(32)
            };
        }
    }
}