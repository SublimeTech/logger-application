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
        private readonly IRepository _repository;

        public LogService(IRepository repository)
        {
            _repository = repository;
        }

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