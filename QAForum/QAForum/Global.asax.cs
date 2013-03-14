
namespace QAForum
{
    using System.ServiceModel;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.Wcf;
    using Filters;
    using ForumServiceContract;
    using Infrastructure;
    using Mapping;
    using Providers;
    using Tags;

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
                "Search Route",
                "tagged/{tag}",
                new {controller = "Post", action = "TagSearch", tag = UrlParameter.Optional}
                );

            routes.MapRoute(
                "Posted Date",
                "posted/{day}-{month}-{year}",
                new {controller = "Post", action = "DateSearch"},
                new {day = @"\d{2}", month = @"\d{2}", year = @"\d{4}"}
                );

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
            AutoMapper.Mapper.AssertConfigurationIsValid();
        }

        private static void ConfigureIoC()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof (MvcApplication).Assembly);
            builder.RegisterType<EntityFrameworkDiagnosticsContext>().As<DiagnosticsContext>();
            builder.RegisterType<SqlPostsTagCloudProvider>().As<TagCloudProvider>();
            builder.RegisterType<ServiceWrapperForumProvider>().As<ForumProvider>();
            builder.Register(c => new ChannelFactory<IForumService>(
                                       new BasicHttpBinding(),
                                       new EndpointAddress("http://localhost:50012/ForumService.svc")))
                   .SingleInstance();
            builder.Register(c => c.Resolve<ChannelFactory<IForumService>>().CreateChannel())
                   .UseWcfSafeRelease();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}