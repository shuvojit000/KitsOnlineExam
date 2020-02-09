using Lincoln.Admin.Web.Controllers;
using Lincoln.OnlineExam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lincoln.Admin.Web.Areas.Faculty.Controllers
{
    public class FacultyController : BaseController
    {
        private readonly IOnlineExam onlineExamService;

        public FacultyController(IOnlineExam onlineExamService)
        {
            this.onlineExamService = onlineExamService;
        }

        // GET: Faculty/Faculty
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}