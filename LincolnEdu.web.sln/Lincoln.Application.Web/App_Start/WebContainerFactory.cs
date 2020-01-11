using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Lincoln.OnlineExam;

namespace Lincoln.Application.Web.App_Start
{
    public class WebContainerFactory
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register dependencies in filter attributes
            //builder.RegisterFilterProvider();

            // Register dependencies in custom views
            //builder.RegisterSource(new ViewRegistrationSource());

            // Register our Data dependencies
            // builder.RegisterModule(new DataModule("MVCWithAutofacDB"));

            builder.RegisterType<OnlineExamService>().As<IOnlineExam>();

            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}