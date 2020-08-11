using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Lincoln.OnlineExam;
using Lincoln.Utility.EmailSending;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;

namespace Lincoln.Admin.Web.App_Start
{
    public class AdminContainerFactory
    {
        public static void ConfigureContainer()
        {
           
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register dependencies in filter attributes
            //builder.RegisterFilterProvider();

            // Register dependencies in custom views
            //builder.RegisterSource(new ViewRegistrationSource());

            // Register our Data dependencies
            // builder.RegisterModule(new DataModule("MVCWithAutofacDB"));

            builder.RegisterType<OnlineExamService>().As<IOnlineExam>();
            builder.RegisterType<EmailSender>().As<IEmailSender>();

            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;
        }
    }
}