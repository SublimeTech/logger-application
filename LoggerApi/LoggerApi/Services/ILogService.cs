using LoggerApi.Models;

namespace LoggerApi.Services
{
    public interface ILogService
    {
        /// <summary>
        /// Logs a new message.
        /// </summary>
        /// <param name="model">The log model.</param>
        /// <returns></returns>
        int Log(LogModel model);
    }
}