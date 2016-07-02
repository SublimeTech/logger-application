using LoggerApi.Models.Entities;

namespace LoggerApi.Models.Mappers
{
    public interface IApplicationMapper
    {
        Application MappTo(ApplicationModel model);
        ApplicationModel MappTo(Application model);
    }
}