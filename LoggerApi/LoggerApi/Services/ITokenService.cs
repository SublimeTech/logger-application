using System;
using LoggerApi.Models;

namespace LoggerApi.Services
{
    public interface ITokenService
    {
        #region Interface member methods.
        /// <summary>
        ///  Function to generate unique token with expiry against the provided applicationId.
        ///  Also add a record in database for generated token.
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        TokenModel GenerateToken(string applicationId);

        /// <summary>
        /// Function to validate token against expiry and existance in database.
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        bool ValidateToken(Guid tokenId);

        /// <summary>
        /// Method to kill the provided token id.
        /// </summary>
        /// <param name="tokenId"></param>
        bool Kill(string tokenId);
        #endregion
    }
}