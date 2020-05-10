using Lincoln.Admin.Web.Models;
using Lincoln.OnlineExam.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
namespace Lincoln.Admin.Web.Areas.Admin.Controllers
{
    public partial class AdminController
    {
        #region Online Examination Application

        public ActionResult OnlineExaminationApp()
        {
            var model = new AdminOnlineExaminationViewModel();

            model.AcademicList = onlineExamService.GetDropdownData("Academic").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();
            model.CountryList = new List<SelectListItem>();
            model.CourseList = new List<SelectListItem>();
            model.DepartmentList = new List<SelectListItem>();
            model.ProgramList = new List<SelectListItem>();
            model.ProgYearList = new List<SelectListItem>();
            model.SemisterList = new List<SelectListItem>();

            return View(model);
        }

        private List<AdminOnlineExaminationViewModel> GetAllOnlineExamApp(AdminOnlineExaminationViewModel model)
        {
            var itemSet = new List<AdminOnlineExaminationViewModel>();
            itemSet = onlineExamService.GetAllOnlineExamApp(new OnlineExam.Request.AdminOnlineExamRequestDTO()
            {
                CourseID = model.CourseID,
                DepartmentID = model.DepartmentID,
                ProgrammeVersioningID = model.ProgrammeVersioningID,
                ProgrammeSemesterID = model.ProgrammeSemesterID,
                CountryID = model.CountryID
            })
                .Select(a => new AdminOnlineExaminationViewModel()
                {
                    StudentID = a.StudentID,
                    StudentName = a.StudentName,
                    PaymentStatus = a.Status,
                    CreatedBy = a.CreatedBy
                }).ToList();

            return itemSet;
        }
        public PartialViewResult OnlineExaminationAppList(AdminOnlineExaminationViewModel model)
        {
            return PartialView("_listOnlineExaminationApp", GetAllOnlineExamApp(model));
        }

        [HttpPost]
        public JsonResult SaveOnlineExamination(List<AdminOnlineExaminationViewModel> models)
        {

            XElement xEle = null;
            if (models.Any())
            {
                xEle = new XElement("ExaminationConfigurations",
                        from item in models
                        select new XElement("ExaminationConfiguration",
                                     new XElement("CourseID", item.CourseID),
                                       new XElement("StudentID", item.StudentID),
                                       new XElement("PaymentStatus", item.PaymentStatus),
                                       new XElement("ExaminationDate", item.ExaminationDate),
                                       new XElement("ExaminationTime", item.ExaminationTime),
                                       new XElement("ExaminationDuration", item.ExaminationDuration),
                                       new XElement("EmployeeID", item.EmployeeID),
                                       new XElement("ReviewStatus", item.ReviewStatus),
                                       new XElement("MarksObtained", item.MarksObtained?? default(decimal))
                                   ));
            }

            var result = onlineExamService.SaveExaminationConfiguration(new OnlineExam.Request.AdminOnlineExamRequestDTO()
            {
                CourseID = models.FirstOrDefault().CourseID,
                ExaminationXML = xEle.ToString().Trim(),
                CreatedBy = User.UserId
            }, "Application");

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion


        #region Examination Scheduling

        public ActionResult ExamSchedule()
        {
            var model = new AdminOnlineExaminationViewModel();

            model.AcademicList = onlineExamService.GetDropdownData("Academic").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();
            model.CountryList = new List<SelectListItem>();
            model.CourseList = new List<SelectListItem>();
            model.DepartmentList = new List<SelectListItem>();
            model.ProgramList = new List<SelectListItem>();
            model.ProgYearList = new List<SelectListItem>();
            model.SemisterList = new List<SelectListItem>();
            var EmpList = onlineExamService.GetAllEmployee().Where(a => a.Status == "A").ToList();
            model.EmployeeList = EmpList?.Select(a => new SelectListItem() { Text = a.EmployeeName.ToString(), Value = a.EmployeeID.ToString() })
                .ToList().GroupBy(n => new { n.Text, n.Value })
                                       .Select(g => g.FirstOrDefault())
                                       .ToList();
            return View(model);
        }

        private List<AdminOnlineExaminationViewModel> GetAllExamSchedule(AdminOnlineExaminationViewModel model)
        {
            var itemSet = new List<AdminOnlineExaminationViewModel>();
            itemSet = onlineExamService.GetAllOnlineExamSchedule(new OnlineExam.Request.AdminOnlineExamRequestDTO()
            {
                CourseID = model.CourseID,
                DepartmentID = model.DepartmentID,
                ProgrammeVersioningID = model.ProgrammeVersioningID,
                ProgrammeSemesterID = model.ProgrammeSemesterID,
                CountryID = model.CountryID
            })
                .Select(a => new AdminOnlineExaminationViewModel()
                {
                    StudentID = a.StudentID,
                    StudentName = a.StudentName,
                    Status = a.Status,
                    CreatedBy = a.CreatedBy
                }).ToList();

            return itemSet;
        }
        public PartialViewResult ExamScheduleList(AdminOnlineExaminationViewModel model)
        {
            return PartialView("_listExamSchedule", GetAllExamSchedule(model));
        }


        #endregion


        #region Evaluation Allotment

        public ActionResult EvaluationAllotment()
        {
            var model = new AdminOnlineExaminationViewModel();

            model.AcademicList = onlineExamService.GetDropdownData("Academic").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();
            model.CountryList = new List<SelectListItem>();
            model.CourseList = new List<SelectListItem>();
            model.DepartmentList = new List<SelectListItem>();
            model.ProgramList = new List<SelectListItem>();
            model.ProgYearList = new List<SelectListItem>();
            model.SemisterList = new List<SelectListItem>();

            return View(model);
        }

        private List<AdminOnlineExaminationViewModel> GetAllEvaluationAllotment(AdminOnlineExaminationViewModel model)
        {
            var itemSet = new List<AdminOnlineExaminationViewModel>();
            itemSet = onlineExamService.GetAllOnlineExamEvaluation(new OnlineExam.Request.AdminOnlineExamRequestDTO()
            {
                CourseID = model.CourseID,
                DepartmentID = model.DepartmentID,
                ProgrammeVersioningID = model.ProgrammeVersioningID,
                ProgrammeSemesterID = model.ProgrammeSemesterID,
                CountryID = model.CountryID
            })
                .Select(a => new AdminOnlineExaminationViewModel()
                {
                    StudentID = a.StudentID,
                    StudentName = a.StudentName,
                    Status = a.Status,
                    CreatedBy = a.CreatedBy
                }).ToList();

            return itemSet;
        }
        public PartialViewResult EvaluationAllotmentList(AdminOnlineExaminationViewModel model)
        {
            return PartialView("_listEvaluationAllotment", GetAllEvaluationAllotment(model));
        }


        #endregion


        #region Result Approval

        public ActionResult ResultApproval()
        {
            var model = new AdminOnlineExaminationViewModel();

            model.AcademicList = onlineExamService.GetDropdownData("Academic").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();
            model.CountryList = new List<SelectListItem>();
            model.CourseList = new List<SelectListItem>();
            model.DepartmentList = new List<SelectListItem>();
            model.ProgramList = new List<SelectListItem>();
            model.ProgYearList = new List<SelectListItem>();
            model.SemisterList = new List<SelectListItem>();

            return View(model);
        }

        private List<AdminOnlineExaminationViewModel> GetAllResultApproval(AdminOnlineExaminationViewModel model)
        {
            var itemSet = new List<AdminOnlineExaminationViewModel>();
            itemSet = onlineExamService.GetAllOnlineExamResult(new OnlineExam.Request.AdminOnlineExamRequestDTO()
            {
                CourseID = model.CourseID,
                DepartmentID = model.DepartmentID,
                ProgrammeVersioningID = model.ProgrammeVersioningID,
                ProgrammeSemesterID = model.ProgrammeSemesterID,
                CountryID = model.CountryID
            })
                .Select(a => new AdminOnlineExaminationViewModel()
                {
                    StudentID = a.StudentID,
                    StudentName = a.StudentName,
                    Status = a.Status,
                    CreatedBy = a.CreatedBy
                }).ToList();

            return itemSet;
        }
        public PartialViewResult ResultApprovalList(AdminOnlineExaminationViewModel model)
        {
            return PartialView("_listResultApproval", GetAllResultApproval(model));
        }


        #endregion 
    }
}