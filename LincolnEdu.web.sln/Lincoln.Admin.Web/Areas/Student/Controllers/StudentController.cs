using Lincoln.Admin.Web.Controllers;
using Lincoln.Admin.Web.Models;
using Lincoln.Framework.Common;
using Lincoln.OnlineExam;
using Lincoln.Utility.EmailSending;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Lincoln.Admin.Web.FilterConfig;

namespace Lincoln.Admin.Web.Areas.Student.Controllers
{
    [AuthorizeAccessAttribute]
    public class StudentController : BaseController
    {
        private readonly IOnlineExam onlineExamService;
        private readonly IEmailSender emailSender;

        public StudentController(IOnlineExam onlineExamService, IEmailSender EmailSender)
        {
            this.onlineExamService = onlineExamService;
            this.emailSender = EmailSender;
        }

        public ActionResult Dashboard()
        {
            var model = new List<StudentDashboardViewModel>();
            model = onlineExamService.GetStudentExamination(new OnlineExam.Request.OnlineTestRequestDTO()
            {
                LoginID = User.UserId
            }).Select(a => new StudentDashboardViewModel()
            {
                CourseID = a.CourseID,
                LoginID = a.LoginID,
                CourseCode = a.CourseCode,
                CourseName = a.CourseName,
                EndDate = a.EndDate,
                ExamAttendance = a.ExamAttendance,
                ExaminationName = a.ExaminationName,
                SLNo = a.SLNo,
                StartDate = a.StartDate,
                Status = a.Status,
                StudentName = a.StudentName,
                ExaminationDuration = a.ExaminationDuration,
                ExaminationDate = a.ExaminationDate,
                TotalMarks = a.TotalMarks,
                MarksObtained = a.MarksObtained
            }).ToList();

            return View(model);
        }


        public ActionResult ExaminationView(string id)
        {
            var model = new StudentExamViewModel();
            var questionSectionViewmodel = new List<ExamQuestionSectionViewModel>();
            if (!string.IsNullOrEmpty(id))
            {
                var returnID = CryptoSecurity.Decrypt(id);

                model = onlineExamService.GetStudentExamination(new OnlineExam.Request.OnlineTestRequestDTO()
                {
                    LoginID = User.UserId
                }).Where(a => a.CourseID == Convert.ToInt32(returnID)).Select(a => new StudentExamViewModel()
                {
                    ExaminationName = a.ExaminationName,
                    CourseID = a.CourseID,
                    TotalMarks = a.TotalMarks,
                    StudentName = a.StudentName,
                    ExaminationDuration = a.ExaminationDuration,
                    IsCalculator = a.IsCalculator,
                    ExaminationID = a.ExaminationID,
                    TimerTime = a.TimerTime

                }).FirstOrDefault();
                TempData["IsCalculator"] = model.IsCalculator;
                if (string.IsNullOrEmpty(model.TimerTime))
                {
                    int totalSeconds = (int)model.ExaminationDuration * 60;
                    int hours = totalSeconds / 3600;
                    int minutes = (totalSeconds % 3600) / 60;
                    int seconds = (totalSeconds % 60);
                    if (hours > 0)
                    {
                        model.ExamHour = hours.ToString("D2");
                        model.ExamMin = minutes.ToString("D2");
                        model.ExamSecond = seconds.ToString("D2");
                    }
                    else if (minutes > 0)
                    {
                        model.ExamHour = "00";
                        model.ExamMin = minutes.ToString("D2");
                        model.ExamSecond = seconds.ToString("D2");
                    }
                    else
                    {
                        model.ExamHour = "00";
                        model.ExamMin = "00";
                        model.ExamSecond = seconds.ToString("D2");
                    }
                }
                else
                {
                    var times = model.TimerTime.Split(':');
                    model.ExamHour = times[0];
                    model.ExamMin = times[1];
                    model.ExamSecond = times[2];
                }

                questionSectionViewmodel = onlineExamService.GetExamQuestionSection(new OnlineExam.Request.ExamQuestionSectionRequestDTO()
                {
                    CourseID = Convert.ToInt32(returnID)
                }).
                        Select(a => new ExamQuestionSectionViewModel()
                        {
                            ExaminationSectionID = a.ExaminationSectionID,
                            MaxQuestionNo = a.MaxQuestionNo,
                            MinQuestionNo = a.MinQuestionNo,
                            SectionName = a.SectionName
                        }).ToList();

            }

            var tupleData = new Tuple<StudentExamViewModel, List<ExamQuestionSectionViewModel>>(model, questionSectionViewmodel);
            return View(tupleData);

        }

        public PartialViewResult QuestionSheet(string courseID, string questionNo, string sectionName)
        {
            var model = new QuestionSetUpViewModel();

            var anseredQuestion = onlineExamService.GetExaminationTest(new OnlineExam.Request.ExaminationTestRequestDTO()
            {
                LoginID = Convert.ToInt32(User.UserId),
                QuestionNo = Convert.ToInt32(questionNo),
                IsAnswer = null
            }).FirstOrDefault();

            model = onlineExamService.GetQuestionPaper(new OnlineExam.Request.ExamQuestionSectionRequestDTO()
            {
                CourseID = Convert.ToInt32(courseID),
                QuestionNo = Convert.ToInt32(questionNo),
                SectionName = sectionName
            })
                .Select(a => new QuestionSetUpViewModel()
                {
                    PaperDetailsID = a.PaperDetailsID,
                    PaperID = a.PaperID,
                    QuestionType = a.QuestionType,
                    QuestionText = a.QuestionText,
                    OptionANo = a.OptionANo,
                    OptionAText = a.OptionAText,
                    OptionBNo = a.OptionBNo,
                    OptionBText = a.OptionBText,
                    OptionCNo = a.OptionCNo,
                    OptionCText = a.OptionCText,
                    OptionDNo = a.OptionDNo,
                    OptionDText = a.OptionDText,
                    OptionENo = a.OptionENo,
                    OptionEText = a.OptionEText,
                    SectionName = a.SectionName,
                    AnswerNo = anseredQuestion?.AnswerNo,
                    AnswerText = anseredQuestion?.AnswerText,
                    IsAnswer = anseredQuestion?.IsAnswer ?? 0,
                    QuestionMarks = a.QuestionMarks
                }).FirstOrDefault();


            return PartialView("_QuestionSheet", model);
        }

        public PartialViewResult LeftSuggestionPanel(string id)
        {
            var model = new ExamLeftPanelViewModel();

            var item = onlineExamService.GetAttemptQuestion(new OnlineExam.Request.ExaminationTestRequestDTO()
            {
                LoginID = User.UserId,
                CourseID = Convert.ToInt32(id)
            }, "GET")
                 .Select(a => new ExamLeftPanelViewModel()
                 {
                     AnswerTotal = a.AnswerTotal,
                     FlagTotal = a.FlagTotal,
                     QuestionNo = a.QuestionNo,
                     QuestionTotal = a.QuestionTotal,
                     StudentID = a.StudentID,
                     CoureseID = Convert.ToInt32(id),
                 })
                 .FirstOrDefault();
            model.CoureseID = Convert.ToInt32(id);
            model.AnswerTotal = item?.AnswerTotal ?? 0;
            model.FlagTotal = item?.FlagTotal ?? 0;
            model.QuestionTotal = item?.QuestionTotal ?? 0;
            model.StudentID = item?.StudentID;
            model.NotAnsweredList = new List<int>();
            model.FlagedList = new List<int>();
            model.CompletedList = new List<int>();
            model.NotAnsweredList = onlineExamService.GetAttemptQuestion(new OnlineExam.Request.ExaminationTestRequestDTO()
            {
                LoginID = User.UserId,
                CourseID = Convert.ToInt32(id)
            }, "NOTANSWER")?.OrderBy(a => a.QuestionNo).Select(a => Convert.ToInt32(a.QuestionNo)).ToList();
            model.FlagedList = onlineExamService.GetAttemptQuestion(new OnlineExam.Request.ExaminationTestRequestDTO()
            {
                LoginID = User.UserId,
                CourseID = Convert.ToInt32(id)
            }, "FLAG")?.OrderBy(a => a.QuestionNo).Select(a => Convert.ToInt32(a.QuestionNo)).ToList();
            model.CompletedList = onlineExamService.GetAttemptQuestion(new OnlineExam.Request.ExaminationTestRequestDTO()
            {
                LoginID = User.UserId,
                CourseID = Convert.ToInt32(id)
            }, "COMPLETED")?.OrderBy(a => a.QuestionNo).Select(a => Convert.ToInt32(a.QuestionNo)).ToList();

            return PartialView("_LeftSuggestionPanel", model);
        }

        public PartialViewResult HeaderButton(string courseID, string questionNo)
        {
            var model = new ExamHeaderButtonViewModel();
            model.CourseID = Convert.ToInt32(courseID);
            var index = Convert.ToInt32(questionNo);
            var total = onlineExamService.GetExamQuestionSection(new OnlineExam.Request.ExamQuestionSectionRequestDTO()
            {
                CourseID = Convert.ToInt32(courseID)
            }).OrderByDescending(a => a.ExaminationSectionID).FirstOrDefault().MaxQuestionNo.Value;
            model.Current = index == 0 ? 1 : index;
            model.Total = total;
            model.Previous = (index - 1) <= 0 ? 1 : index - 1;
            model.Next = index + 1 > total ? index : index + 1;
            model.FirstLengthStart = index - 14 <= 0 ? 1 : index - 14;
            model.FirstLengthEnd = (index - 1) < 0 ? 0 : index - 1;
            model.SecondLengthStart = index + 1 > total ? index : index + 1;
            model.SecondLengthEnd = index == total ? 0 : index + 14 > total ? total : index + 14;

            return PartialView("_HeaderButton", model);
        }

        public PartialViewResult CalculatorWindow()
        {
            return PartialView("_CalculatorWindow");
        }

        [HttpPost]
        public JsonResult SaveExaminationSheet(QuestionSetUpViewModel model)
        {
            var result = onlineExamService.SaveExaminationSheet(new OnlineExam.Request.StudentExaminationSheetResponseDTO()
            {

                LoginID = User.UserId,
                PaperDetailsID = model.PaperDetailsID,
                PaperID = model.PaperID,
                ExaminationDuration = model.ExaminationDuration,
                TotalTime = model.TotalTime
            }, "INSERT");

            //if (result == 1)
            //{
            //    emailSender.SendHtmlEmailAsync("Test Subject", "Test Body", "", "", null);
            //}

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult SaveExaminationTest(QuestionSetUpViewModel model)
        {
            return Json(onlineExamService.SaveExaminationTest(new OnlineExam.Request.ExaminationTestRequestDTO()
            {
                AnswerNo = model.AnswerNo,
                AnswerText = model.AnswerText,
                IsAnswer = model.IsAnswer == -1 ? 0 : 1,
                LoginID = User.UserId,
                QuestionNo = model.QuestionNo
            }, model.IsAnswer > 0 ? "UPDATE" : "INSERT"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateExamTimer(string examinationId, string courseId, string timerTime)
        {
            return Json(onlineExamService.SaveTimerTime(new OnlineExam.Request.ExaminationTestRequestDTO()
            {
                LoginID = User.UserId,
                CourseID = Convert.ToInt32(courseId),
                ExaminationID = Convert.ToInt32(examinationId),
                TimerTime = timerTime

            }), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Capture(string name)
        {
            /******************* Examination & Course ************************/
            var model = new StudentExamViewModel();
            model = onlineExamService.GetStudentExamination(new OnlineExam.Request.OnlineTestRequestDTO()
            {
                LoginID = User.UserId
            }).Select(a => new StudentExamViewModel()
            {
                ExaminationName = a.ExaminationName,
                CourseName = a.CourseName,
            }).FirstOrDefault();

            var files = HttpContext.Request.Files;
            if (files != null)
            {
                foreach (var _file in files)
                {
                    HttpPostedFileBase file = Request.Files[_file.ToString()];
                    if (file.ContentLength > 0)
                    {
                        var webcamPath = ConfigurationManager.AppSettings["WebcamPath"].ToString() + model.ExaminationName + "\\" + model.CourseName;
                        //var folderPath = Path.Combine(Server.MapPath("~/CameraPhotos/"), User.UserName);
                        var folderPath = Path.Combine(webcamPath, User.UserName);
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                        var fileName = DateTime.Now.ToString("dd-MM-yyyy") + "_" + DateTime.Now.ToString("hh-mm-ss") + "_" + DateTime.Now.ToString("tt") + "_" + User.UserId.ToString() + System.IO.Path.GetExtension(file.FileName);
                        var path = Path.Combine(folderPath + "\\", fileName);
                        file.SaveAs(path);
                    }
                }
                return Json(true);
            }
            else
            {
                return Json(false);
            }

        }

    }
}