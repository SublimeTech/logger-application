using LoggerApi.Models;

namespace LoggerApi.Services
{
    public interface IApplicationService
    {
        ApplicationModel CreateNewApplication(string displayName);
    }
}