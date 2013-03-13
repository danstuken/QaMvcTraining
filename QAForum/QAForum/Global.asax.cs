using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QAForum
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using Filters;
    using Filters.Diagnostics;
    using Infrastructure;
    using Mapping;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            var diagnosticFilterProvider = new DiagnosticsFilterProvider();
            diagnosticFilterProvider.Add("*", "Index");
            diagnosticFilterProvider.Add("Post", "Create");
            diagnosticFilterProvider.Add("Post", "Edit");
            FilterProviders.Providers.Add(diagnosticFilterProvider);
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            ConfigureIoC();
            ConfigureMappings();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        private static void ConfigureMappings()
        {
            ViewModelMapping.MapModelsToViewModels();
            ViewModelMapping.MapViewModelsToModels();
        }

        private static void ConfigureIoC()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof (MvcApplication).Assembly);
            builder.RegisterType<EntityFrameworkForumContext>().As<ForumContext>().InstancePerLifetimeScope();
            builder.RegisterType<SqlForumRepository>().As<ForumRepository>();
            builder.RegisterType<EntityFrameworkDiagnosticsContext>().As<DiagnosticsContext>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}