[assembly: WebActivator.PostApplicationStartMethod(typeof(Sample.Web.MVC.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace Sample.Web.MVC.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using Sample.Repository.Interface;
    using Sample.Domain;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);

            string repositoryType = MvcApplication.RepositoryType;

            container.Register
            (
                typeof(IEmployeeRepository),
                ReflectionHelper.GetType(repositoryType, string.Concat(repositoryType, ".EmployeeRepository"))
            );

            container.Register
            (
                typeof(IExchangeRateRepository),
                ReflectionHelper.GetType(repositoryType, string.Concat(repositoryType, ".ExchangeRateRepository"))
            );
        }
    }
}