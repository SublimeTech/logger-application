using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LoggerApi.Models.Entities;
using LoggerApi.Models.Repositories;

namespace LoggerApi.Controllers
{
    public class RegisterController : ApiController
    {
        private IRepository _repository;

        public RegisterController(IRepository repository)
        {
            _repository = repository;
        }

        public IHttpActionResult Get(int id)
        {
            var log =_repository.GetById<Log>(id);
            return Ok(log);
        }

        public string Post()
        {
            return null;
        }
    }
}
