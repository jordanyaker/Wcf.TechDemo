namespace TechDemo.Web {
    using System.Reflection;
    using System.Web.Mvc;
    using Autofac;
    using Autofac.Configuration;
    using Autofac.Integration.Mvc;
    using Microsoft.Practices.ServiceLocation;
    using TechDemo.Bootstrapper;
    using TechDemo.IoC.Autofac;

    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));

            BootstrapManager.Run();
        }
    }
}