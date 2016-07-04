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

        public TokenModel GenerateToken(string applicationId)
        {
            var token = Guid.NewGuid();
            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now.AddSeconds(900); //TODO: Configure this in DB
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

        public bool Kill(string tokenId)
        {
            var token = _repository.GetById<Token>(tokenId);
            _repository.Delete(token);
            return true;
        }
    }
}