namespace QADataServices
{
    using System;
    using System.Reflection;
    using Autofac;
    using Autofac.Integration.Wcf;
    using CachingLayer;
    using ForumServiceContract;

    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ConfigureIoC();
        }

        private static void ConfigureIoC()
        {
            var builder = new ContainerBuilder();
            var dbAssembly = Assembly.LoadFrom(@"C:\QAASPMVC-3\Exercises\QAForum\EntityFrameworkDataLayer\bin\Debug\EntityFrameworkDataLayer.dll");
            builder.RegisterAssemblyTypes(dbAssembly)
                   .AsImplementedInterfaces();
            //builder.RegisterType<ForumService>().Named<IForumService>("ForumService");
            //builder.RegisterDecorator<IForumService>(
            //    (c, inner) => new CachingForumService(inner),
            //    fromKey: "ForumService"
            //    );
            builder.RegisterType<ForumService>().As<IForumService>();
            AutofacHostFactory.Container = builder.Build();
            var x = AutofacHostFactory.Container.Resolve<IForumService>();
        }
    }
}