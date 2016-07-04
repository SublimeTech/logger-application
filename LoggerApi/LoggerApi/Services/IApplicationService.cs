using LoggerApi.Models;

namespace LoggerApi.Services
{
    public interface IApplicationService
    {
        /// <summary>
        /// Creates a new Application.
        /// </summary>
        /// <param name="displayName">The name of the application.</param>
        /// <returns></returns>
        ApplicationModel CreateNewApplication(string displayName);

        /// <summary>
        /// Checks if an application is authenticated.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <param name="secret">The application secret.</param>
        /// <returns></returns>
        string Authenticate(string applicationId, string secret);
    }
}