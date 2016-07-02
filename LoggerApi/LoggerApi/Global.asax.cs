using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using LoggerApi.Controllers;
using LoggerApi.Infrastructure;
using LoggerApi.Models.Entities;
using LoggerApi.Models.Repositories;
using LoggerApi.Services;
using Microsoft.ApplicationInsights.DataContracts;
using NHibernate;
using Ninject;
using WebApiContrib.IoC.Ninject;

namespace LoggerApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // NHibernate configuration
            var configuration = new NHibernate.Cfg.Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof(Log).Assembly);
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();

            // Ninject
            Ninject.IKernel container = new StandardKernel();

            // Set Web API Resolver
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(container);
            container.Bind<ISessionFactory>().ToConstant(sessionFactory);

            container.Bind<ISessionManager>().To<SessionManager>();
            container.Bind<IRepository>().To<GenericRepository>();
            container.Bind<IApplicationService>().To<ApplicationService>();
        }
    }
}