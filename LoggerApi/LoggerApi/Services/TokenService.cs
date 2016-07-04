using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using LoggerApi.Models;
using LoggerApi.Models.Entities;
using LoggerApi.Models.Repositories;

namespace LoggerApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly IRepository _repository;

        public TokenService(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        ///  Function to generate unique token with expiry against the provided applicationId.
        ///  Also add a record in database for generated token.
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        public TokenModel GenerateToken(string applicationId)
        {
            var token = Guid.NewGuid();
            DateTime issuedOn = DateTime.Now;

            var sessionConfig = _repository.WhereAllEq<SessionConfig>(new Dictionary<object, object>()).FirstOrDefault();
            if (sessionConfig == null) return null;

            DateTime expiredOn = DateTime.Now.AddSeconds(sessionConfig.SessionLifeTime); 
            var tokenEntity = new Token
            {
                ApplicationId = applicationId,
                AccessToken = token,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn
            };

            _repository.Save(tokenEntity);
            var tokenModel = new TokenModel()
            {
                ApplicationId = applicationId,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn,
                AccessToken = token
            };
            return tokenModel;
        }

        /// <summary>
        /// Function to validate token against expiry and existance in database.
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public bool ValidateToken(Guid tokenId)
        {
            var filter = new Dictionary<string, Guid> {{"AccessToken", tokenId}};
            var token = _repository.WhereAllEq<Token>(filter).FirstOrDefault();
            if (token != null && !(DateTime.Now > token.ExpiresOn))
            {
                token.ExpiresOn = token.ExpiresOn.AddSeconds(900); //TODO: Configure this in DB
                _repository.Update(token);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to kill the provided token id.
        /// </summary>
        /// <param name="tokenId"></param>
        public bool Kill(string tokenId)
        {
            var token = _repository.GetById<Token>(tokenId);
            _repository.Delete(token);
            return true;
        }
    }
}