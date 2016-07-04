using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoggerApi.Models;
using LoggerApi.Models.Entities;
using LoggerApi.Models.Mappers;
using LoggerApi.Models.Repositories;
using MlkPwgen;

namespace LoggerApi.Services
{
    public class ApplicationService : IApplicationService
    {
        /// <summary>
        /// Repository that handles all the DB operations.
        /// </summary>
        /// <returns></returns>
        private readonly IRepository _repository;

        /// <summary>
        /// Service that map application models with it entity.
        /// </summary>
        /// <returns></returns>
        private readonly IApplicationMapper _mapper;

        public ApplicationService(IRepository repository, IApplicationMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new Application.
        /// </summary>
        /// <param name="displayName">The name of the application.</param>
        /// <returns></returns>
        public ApplicationModel CreateNewApplication(string displayName)
        {
            if (string.IsNullOrEmpty(displayName))
            {
                return null;
            }

            var model = new ApplicationModel()
            {
                DisplayName     = displayName,
                ApplicationId   = PasswordGenerator.Generate(32),
                Secret          = PasswordGenerator.Generate(25)
            };

            var application = _mapper.MappTo(model);
            try
            {
                _repository.Save(application);
            }
            catch (Exception ex)
            {
                return null;
            }
            return model;
        }

        /// <summary>
        /// Checks if an application is authenticated.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <param name="secret">The application secret.</param>
        /// <returns></returns>
        public string Authenticate(string applicationId, string secret)
        {
            var application = _repository.GetById<Application>(applicationId);
            if (application == null || !application.Secret.Equals(secret))
            {
                return null;
            }
            return application.ApplicationId;
        }
    }
}