using System;
using System.Web;
using System.Web.Mvc;

namespace Lincoln.Application.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomExceptionHandlerFilter());
        }
        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
        public class CutomAuthorizeAttribute : AuthorizeAttribute
        {
           
            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                base.OnAuthorization(filterContext);
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    filterContext.Result = new RedirectResult("~/Home/LogIn");
                    return;
                }

                if (filterContext.Result is HttpUnauthorizedResult)
                {
                    filterContext.Result = new RedirectResult("~/Home/AccessDenied");
                    return;
                }
            }
        }

        public class CustomExceptionHandlerFilter : FilterAttribute, IExceptionFilter
        {
            public void OnException(ExceptionContext filterContext)
            {
                //ExceptionLogger logger = new ExceptionLogger()
                //{
                //    ExceptionMessage = filterContext.Exception.Message,
                //    ExceptionStackTrack = filterContext.Exception.StackTrace,
                //    ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                //    ActionName = filterContext.RouteData.Values["action"].ToString(),
                //    ExceptionLogTime = DateTime.Now
                //};
                //EmployeeContext dbContext = new EmployeeContext();
                //dbContext.ExceptionLoggers.Add(logger);
                //dbContext.SaveChanges();

                filterContext.ExceptionHandled = true;
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error"
                };
            }
        }
    }

}
