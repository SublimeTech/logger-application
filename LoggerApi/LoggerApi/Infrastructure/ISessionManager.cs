using NHibernate;

namespace LoggerApi.Infrastructure
{
    public interface ISessionManager
    {
        ISession CurrentSession { get; set; }
    }
}