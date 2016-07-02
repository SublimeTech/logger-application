using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;

namespace LoggerApi.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private readonly ISessionFactory _sessionFactory;

        public SessionManager(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        private ISession _session;
        public ISession CurrentSession
        {
            get { return _session ?? (_session = _sessionFactory.OpenSession()); }
            set { _session = value; }
        }
    }
}