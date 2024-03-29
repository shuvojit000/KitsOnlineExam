﻿using Lincoln.Admin.Web.Models;
using Lincoln.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    Status = a.Status,
                    PaymentStatus = a.PaymentStatus,
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
                                         new XElement("EnrollmentNo", item.EnrollmentNo),
                                          new XElement("IntakeID", item.IntakeID),
                                         new XElement("EmailID", item.EmailID),
                                       new XElement("PaymentStatus", item.PaymentStatus),
                                       new XElement("ExaminationDate", item.ExaminationDate),
                                       new XElement("ExaminationTime", item.ExaminationTime),
                                       new XElement("ExaminationDuration", item.ExaminationDuration),
                                       new XElement("EmployeeID", item.EmployeeID),
                                       new XElement("ReviewStatus", item.ReviewStatus),
                                       new XElement("MarksObtained", item.MarksObtained ?? default(decimal)),
                                       new XElement("ExaminationID", item.ExaminationID ?? default(int))
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
            model.IntakeList = onlineExamService.GetDropdownData("Intake").Select(a => new SelectListItem { Text = a.CodeDesc, Value = a.CodeID }).ToList();
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
            model.ExaminationList = GetAllExaminationName().Select(a => new SelectListItem() { Text = a.ExaminationName.ToString(), Value = a.ExaminationNameID.ToString() })
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
                CountryID = model.CountryID,
                EmployeeID = model.EmployeeID
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

        [HttpPost]
        public JsonResult SaveOnlineExaminationSchedule(AdminOnlineExaminationViewModel model)
        {
            var models = new List<AdminOnlineExaminationViewModel>();

            var studentlst = model.CustomList;
            if (studentlst.Any())
            {
                models.AddRange(studentlst.Select(a => new AdminOnlineExaminationViewModel()
                {
                    CourseID = model.CourseID,
                    EnrollmentNo = a.EnrollmentNo, // Student ID
                    ExaminationDate = model.ExaminationDate,
                    ExaminationTime = model.ExaminationTime,
                    ExaminationDuration = model.ExaminationDuration,
                    ExaminationID = model.ExaminationID,
                    IntakeID = model.IntakeID,
                    EmailID = a.EmailID,
                    StudentName = a.StudentName,
                    IsCalculator = model.IsCalculator
                }));
            }

            XElement xEle = null;
            if (models.Any())
            {
                xEle = new XElement("ExaminationConfigurations",
                from item in models
                select new XElement("ExaminationConfiguration",
                new XElement("CourseID", item.CourseID),
                new XElement("StudentID", item.StudentID),
                new XElement("EnrollmentNo", item.EnrollmentNo),
                new XElement("IntakeID", item.IntakeID),
                new XElement("EmailID", item.EmailID),
                new XElement("StudentName", item.StudentName),
                new XElement("PaymentStatus", item.PaymentStatus),
                new XElement("ExaminationDate", item.ExaminationDate),
                new XElement("ExaminationTime", item.ExaminationTime),
                new XElement("ExaminationDuration", item.ExaminationDuration),
                new XElement("EmployeeID", item.EmployeeID),
                new XElement("ReviewStatus", item.ReviewStatus),
                new XElement("IsCalculator", item.IsCalculator),
                new XElement("MarksObtained", item.MarksObtained ?? default(decimal)),
                new XElement("ExaminationID", item.ExaminationID ?? default(int))
                ));
            }

            var result = onlineExamService.SaveExaminationConfiguration(new OnlineExam.Request.AdminOnlineExamRequestDTO()
            {
                CourseID = models.FirstOrDefault().CourseID,
                ExaminationXML = xEle.ToString().Trim(),
                CreatedBy = User.UserId
            }, "Scheduling");

            if (result == 1)
            {
                var bccEmail = new List<EmailViewModel>();
                bccEmail = onlineExamService.GetEmail().Select(a => new EmailViewModel()
                {
                    Email = a.Email,
                }).ToList();

                /*************** Please Change Subject & Body Message *********************/
                var subject = "Examination Schedule";
                var _content = "";
                foreach (var item in models)
                {
                    var user = onlineExamService.ValidateUser(new OnlineExam.Request.LogInRequestDTO()
                    {
                        EmailID = item.EmailID,
                        UserName = item.EnrollmentNo

                    }, "LOGIN");

                    // _content = null;
                    // _content += header_content();
                    _content += "<div><b>Dear " + item.StudentName + ",</b></div><br />";
                    _content += "<div>You are now registered as a Ph.D scholar under Lincoln University College, Malaysia.</div><br />";
                    _content += "An account has been created for you in our system by your student id. <br />";
                    _content += "To check the details you need to login into your account with the credentials given below</div><br />";
                    _content += "<b>URL : <a href='https://phdresearch.lincolnedu.education/student/stdlogin.aspx' target='_blank'>https://phdresearch.lincolnedu.education/student/stdlogin.aspx<a> </b><br />";
                    _content += "<b>Username : " + user.UserName + "</b><br />";
                    _content += "<b>Password : " + user.Password + " </ b >< br /> ";
                    _content += "<div>For any other assistance please mail us at <i>postgraduate@lincoln.edu.my</i><br /><br /></div>";

                    _content += "Thanks & Regards,";
                    _content += "<b><br />LUC Online Mail Service</b><br />";
                    var form = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();

                    emailSender.SendHtmlEmailAsync(subject, _content, form, models.Select(a => a.EmailID).AsEnumerable(), null, bccEmail.Select(a => a.Email).AsEnumerable(), null);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetMaximumMarks(AdminOnlineExaminationViewModel model)
        {
            var maxMarks = GetAllExaminationSection().Where(a => a.AcademicID == model.AcademicID
              && a.CourseID == model.CourseID && a.ProgrammeVersioningID == model.ProgrammeVersioningID && a.SemisterCode == model.ProgrammeSemesterID
            ).Sum(a => a.MaximumMarks);

            return Json(maxMarks, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetExaminationNameById(string examinationId)
        {
            return Json(SelectExaminationName(examinationId), JsonRequestBehavior.AllowGet);
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
            var EmpList = onlineExamService.GetAllEmployee().Where(a => a.Status == "A").ToList();
            model.EmployeeList = EmpList?.Select(a => new SelectListItem() { Text = a.EmployeeName.ToString(), Value = a.EmployeeID.ToString() })
                .ToList().GroupBy(n => new { n.Text, n.Value })
                                       .Select(g => g.FirstOrDefault())
                                       .ToList();
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
                    CreatedBy = a.CreatedBy,
                    EmployeeID = a.EmployeeId,
                    EmployeeName = a.EmployeeName
                }).ToList();

            return itemSet;
        }

        public PartialViewResult EvaluationAllotmentList(AdminOnlineExaminationViewModel model)
        {
            return PartialView("_listEvaluationAllotment", GetAllEvaluationAllotment(model));
        }

        [HttpPost]
        public JsonResult SaveEvaluationAllotment(AdminOnlineExaminationViewModel model)
        {

            var models = new List<AdminOnlineExaminationViewModel>();

            var studentlst = GetAllEvaluationAllotment(model);
            if (studentlst.Any())
            {
                models.AddRange(studentlst.Select(a => new AdminOnlineExaminationViewModel()
                {
                    CourseID = model.CourseID,
                    StudentID = a.StudentID,
                    EmployeeID = model.EmployeeID != null && model.EmployeeID != 0 ? model.EmployeeID : a.EmployeeID
                }));
            }

            XElement xEle = null;
            if (models.Any())
            {
                xEle = new XElement("ExaminationConfigurations",
                        from item in models
                        select new XElement("ExaminationConfiguration",
                                     new XElement("CourseID", item.CourseID),
                                       new XElement("StudentID", item.StudentID),
                                         new XElement("EnrollmentNo", item.EnrollmentNo),
                                          new XElement("IntakeID", item.IntakeID),
                                         new XElement("EmailID", item.EmailID),
                                       new XElement("PaymentStatus", item.PaymentStatus),
                                       new XElement("ExaminationDate", item.ExaminationDate),
                                       new XElement("ExaminationTime", item.ExaminationTime),
                                       new XElement("ExaminationDuration", item.ExaminationDuration),
                                       new XElement("EmployeeID", item.EmployeeID),
                                       new XElement("ReviewStatus", item.ReviewStatus),
                                       new XElement("MarksObtained", item.MarksObtained ?? default(decimal)),
                                       new XElement("ExaminationID", item.ExaminationID ?? default(int))
                                   ));
            }

            var result = onlineExamService.SaveExaminationConfiguration(new OnlineExam.Request.AdminOnlineExamRequestDTO()
            {
                CourseID = models.FirstOrDefault().CourseID,
                ExaminationXML = xEle.ToString().Trim(),
                CreatedBy = User.UserId
            }, "Evaluation");

            //if (result == 1)
            //{
            //    emailSender.SendHtmlEmailAsync("Test Subject", "Test Body", "", "", null);
            //}
            return Json(result, JsonRequestBehavior.AllowGet);
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
                    CourseID = a.CourseID,
                    CreatedBy = a.CreatedBy,
                    EmployeeID = a.EmployeeId,
                    EmployeeName = a.EmployeeName,
                    CourseName = a.CourseName
                }).ToList();

            return itemSet;
        }

        public PartialViewResult ResultApprovalList(AdminOnlineExaminationViewModel model)
        {
            return PartialView("_listResultApproval", GetAllResultApproval(model));
        }

        private List<AdminAnswerReviewViewModels> GetAllResultReview(AdminOnlineExaminationViewModel model)
        {
            var itemSet = new List<AdminAnswerReviewViewModels>();
            itemSet = onlineExamService.GetAnserReview(new OnlineExam.Request.AdminOnlineExamRequestDTO()
            {
                CourseID = model.CourseID,
                DepartmentID = model.DepartmentID,
                ProgrammeVersioningID = model.ProgrammeVersioningID,
                ProgrammeSemesterID = model.ProgrammeSemesterID,
                CountryID = model.CountryID,
                StudentID = model.StudentID
            })
                .Select(a => new AdminAnswerReviewViewModels()
                {
                    AnswerNo = a.AnswerNo,
                    AnswerNoByStudent = a.AnswerNoByStudent,
                    AnswerText = a.AnswerText,
                    AnswerTextByStudent = a.AnswerTextByStudent,
                    PaperDetailsID = a.PaperDetailsID,
                    PaperID = a.PaperID,
                    QuestionMarks = a.QuestionMarks,
                    QuestionMarksObtain = a.QuestionMarksObtain,
                    QuestionNo = a.QuestionNo,
                    QuestionText = a.QuestionText,
                    QuestionType = a.QuestionType,
                    MarksObtained = a.MarksObtained,
                    ResultApproved = a.ResultApproved,
                }).ToList();

            return itemSet;
        }

        public ActionResult AnswerSheet(string StudentID, string CourseID, string EmployeeID)
        {

            try
            {
                if (!string.IsNullOrEmpty(StudentID) && !string.IsNullOrEmpty(CourseID))
                {
                    var studentID = StudentID;
                    var courseID = CourseID;
                    var itemslist = GetAllResultReview(new Models.AdminOnlineExaminationViewModel() { StudentID = Convert.ToInt32(studentID), CourseID = Convert.ToInt32(courseID) });

                    ViewBag.QuestionType = itemslist.FirstOrDefault()?.QuestionType;
                    ViewBag.EmployeeID = EmployeeID;
                    ViewBag.CourseID = CourseID;
                    ViewBag.StudentID = StudentID;

                    return View(itemslist);
                }
            }
            catch (Exception)
            {

                return View("Error");
            }
            return View("Error");

        }

        [HttpPost]
        public JsonResult SaveResultApproval(List<AdminOnlineExaminationViewModel> models)
        {
            XElement xEle = null;
            if (models.Any())
            {
                xEle = new XElement("ResultApprovals",
                        from item in models
                        select new XElement("ResultApproval",
                                     new XElement("PaperDetailsID", item.PaperDetailsID),
                                       new XElement("QuestionNo", item.QuestionNo),
                                       new XElement("MarksObtained", item.MarksObtained ?? default(decimal))
                                   ));
            }

            var result = onlineExamService.SaveResultApproval(new OnlineExam.Request.AdminOnlineExamRequestDTO()
            {
                CourseID = Convert.ToInt32(models.FirstOrDefault().strCourseID),
                StudentID = Convert.ToInt32(models.FirstOrDefault().strStudentID),
                EmployeeID = Convert.ToInt32(models.FirstOrDefault().strEmployeeID),
                ExaminationXML = xEle.ToString().Trim(),
                CreatedBy = User.UserId
            });

            //if (result == 1)
            //{
            //    emailSender.SendHtmlEmailAsync("Test Subject", "Test Body", "", "", null);
            //}

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion 
    }
}