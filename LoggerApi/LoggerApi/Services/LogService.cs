using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoggerApi.Models;
using LoggerApi.Models.Entities;
using LoggerApi.Models.Repositories;

namespace LoggerApi.Services
{
    public class LogService : ILogService
    {
        /// <summary>
        /// Repository that handles all the DB operations.
        /// </summary>
        /// <returns></returns>
        private readonly IRepository _repository;

        public LogService(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Logs a new message.
        /// </summary>
        /// <param name="model">The log model.</param>
        /// <returns></returns>
        public int Log(LogModel model)
        {
            var log = new Log()
            {
                ApplicationId = model.ApplicationId,
                Level = model.Level,
                Message = model.Message,
                Logger = model.Logger
            };

            try
            {
                _repository.Save(log);
            }
            catch (Exception ex)
            {
                return 0;
            }
            return log.LogId;
        }
    }
}