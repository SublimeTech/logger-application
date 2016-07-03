using LoggerApi.Models;

namespace LoggerApi.Services
{
    public interface ILogService
    {
        int Log(LogModel model);
    }
}