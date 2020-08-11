using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.SessionState;
using Lincoln.Admin.Web.Controllers;
using Lincoln.Admin.Web.Models;
using Lincoln.OnlineExam;
using Lincoln.Utility.EmailSending;

namespace Lincoln.Admin.Web.Areas.Admin.Controllers
{
    [AuthorizeAccessAttribute]
    public partial class AdminController : BaseController
    {
        private readonly IOnlineExam onlineExamService;
        private readonly IEmailSender emailSender;

        public AdminController(IOnlineExam onlineExamService, IEmailSender emailSender)
        {
            this.onlineExamService = onlineExamService;
            this.emailSender = emailSender;
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ChangeAllStatus(UpdateStatusViewModel model)
        {
            return Json(onlineExamService.UpdateStatus(new OnlineExam.Request.UpdateStatusReuestDTO
            {
                Active = model.Active,
                CreatedBy = User.UserId,
                ID = model.ID,
                Table = model.Table,

            }), JsonRequestBehavior.AllowGet);
        }       

        #region Dropdown Code
        [HttpPost]
        public JsonResult GetProgDrop(string departmentID)
        {

            return Json(onlineExamService.GetAllProgramme().Where(a => a.DepartmentID == Convert.ToInt32(departmentID) && a.Status == "A")
                    .Select(a => new SelectListItem
                    {
                        Text = a.ProgrammeName + "(" + a.ProgrammeCode + ")",
                        Value = a.ProgrammeID.ToString()

                    }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProgDropWithVersion(string departmentID)
        {
            return Json(onlineExamService.GetAllProgrammeWithVersion().Where(a => a.DepartmentID == Convert.ToInt32(departmentID) && a.Status == "A"
                                    && !string.IsNullOrEmpty(a.Version))
                    .Select(a => new SelectListItem
                    {
                        Text = a.ProgrammeName + "(" + a.Version + ")",
                        Value = a.ProgramVersioningID.ToString()

                    }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDepartmentDrop(string academicID)
        {
            return Json(onlineExamService.GetAllDepartment().Where(a => a.AcademicID == Convert.ToInt32(academicID) && a.Status == "A")
                    .Select(a => new SelectListItem
                    {
                        Text = a.DepartmentName + "(" + a.DepartmentCode + ")",
                        Value = a.DepartmentID.ToString()

                    }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProgrammeVersionDDL(string ProgramVID)
        {
            return Json(onlineExamService.GetAllProgramVersioning().Where(a => a.ProgramCode == Convert.ToInt32(ProgramVID) && a.Status == "A")
                      .Select(a => new SelectListItem
                      {
                          Text = a.Version,
                          Value = a.ProgramVersioningID.ToString()

                      }).ToList(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetCourseDDL(string SemisterID)
        {
            return Json(onlineExamService.GetAllCourse().Where(a => a.CourseID == Convert.ToInt32(SemisterID))
                    .Select(a => new SelectListItem
                    {
                        Text = a.CourseName,
                        Value = a.CourseID.ToString()

                    }).ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
