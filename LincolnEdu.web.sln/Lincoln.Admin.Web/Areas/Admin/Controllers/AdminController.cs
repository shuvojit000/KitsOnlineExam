using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lincoln.Admin.Web.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Dashboard()
        {
            return View();
        }

        #region Batch

        public ActionResult Batch() => View();

        #endregion

        #region Course

        public ActionResult Course() => View();

        #endregion

        #region Student Registration

        public ActionResult StudentRegistration() => View();

        #endregion

        #region Subject

        public ActionResult Subject() => View();
        #endregion

        #region Faculty registration

        public ActionResult FacultyRegistration() => View();

        #endregion

    }
}