using System.Web.Mvc;

namespace Lincoln.Admin.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
            "Admin_AnswerScript",
            "Admin/{controller}/{action}/{StudentID}/{CourseID}/{EmployeeID}",
            new { action = "AnswerSheet", StudentID = UrlParameter.Optional, CourseID = UrlParameter.Optional , EmployeeID = UrlParameter.Optional }
        );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}