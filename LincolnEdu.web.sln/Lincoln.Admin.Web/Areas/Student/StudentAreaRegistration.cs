﻿using System.Web.Mvc;

namespace Lincoln.Admin.Web.Areas.Student
{
    public class StudentAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Student";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Student_QuestionSheet",
                "Student/{controller}/{action}/{courseID}/{questionNo}/{sectionName}",
                new { action = "QuestionSheet", courseID = UrlParameter.Optional, questionNo = UrlParameter.Optional, sectionName = UrlParameter.Optional }
            );
            context.MapRoute(
                "Student_default",
                "Student/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}