using Lincoln.Admin.Web.Controllers;
using Lincoln.Admin.Web.Models;
using Lincoln.OnlineExam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static Lincoln.Admin.Web.FilterConfig;

namespace Lincoln.Admin.Web.Areas.Student.Controllers
{
    [CutomAuthorizeAttribute]
    public class StudentController : BaseController
    {
        private readonly IOnlineExam onlineExamService;

        public StudentController(IOnlineExam onlineExamService)
        {
            this.onlineExamService = onlineExamService;
        }

        // GET: Faculty/Faculty
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
                StudentName = a.StudentName
            }).ToList();

            return View(model);
        }


        public ActionResult ExaminationView(string id)
        {
            var model = new StudentExamViewModel();
            var questionSectionViewmodel = new List<ExamQuestionSectionViewModel>();
            model = onlineExamService.GetStudentExamination(new OnlineExam.Request.OnlineTestRequestDTO()
            {
                LoginID = User.UserId
            }).Where(a => a.CourseID == Convert.ToInt32(id)).Select(a => new StudentExamViewModel()
            {
                ExaminationName = a.ExaminationName,
                CourseID = a.CourseID
            }).FirstOrDefault();
            questionSectionViewmodel = onlineExamService.GetExamQuestionSection(new OnlineExam.Request.ExamQuestionSectionRequestDTO()
            {
                CourseID = Convert.ToInt32(id)
            }).
            Select(a => new ExamQuestionSectionViewModel()
            {
                ExaminationSectionID = a.ExaminationSectionID,
                MaxQuestionNo = a.MaxQuestionNo,
                MinQuestionNo = a.MinQuestionNo,
                SectionName = a.SectionName
            }).ToList();
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
                    AnswerNo = anseredQuestion?.AnswerNo,
                    AnswerText = anseredQuestion?.AnswerText,
                    IsAnswer = anseredQuestion?.IsAnswer ?? 0
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
            }, "NOTANSWER")?.Select(a => Convert.ToInt32(a.QuestionNo)).ToList();
            model.FlagedList = onlineExamService.GetAttemptQuestion(new OnlineExam.Request.ExaminationTestRequestDTO()
            {
                LoginID = User.UserId,
                CourseID = Convert.ToInt32(id)
            }, "FLAG")?.Select(a => Convert.ToInt32(a.QuestionNo)).ToList();
            model.CompletedList = onlineExamService.GetAttemptQuestion(new OnlineExam.Request.ExaminationTestRequestDTO()
            {
                LoginID = User.UserId,
                CourseID = Convert.ToInt32(id)
            }, "COMPLETED")?.Select(a => Convert.ToInt32(a.QuestionNo)).ToList();

            return PartialView("_LeftSuggestionPanel", model);
        }
        public PartialViewResult HeaderButton(string courseID, string questionNo)
        {
            var model = new ExamHeaderButtonViewModel();
            model.CourseID = Convert.ToInt32(courseID);
            var index = Convert.ToInt32(questionNo);
            var total = 25;
            model.Current = index==0?1:index;
            model.Total = total;
            model.Previous = (index - 1) <= 0 ? 1 : index - 1;
            model.Next = index + 1 > total ? index : index + 1;
            model.FirstLengthStart = index - 13 <= 0 ? 1 : index - 13;
            model.FirstLengthEnd = (index - 1) < 0 ? 0 : index - 1;
            model.SecondLengthStart = index + 1 > total ? index : index + 1;
            model.SecondLengthEnd = index + 15 > total ? total : index + 15;


            return PartialView("_HeaderButton", model);
        }

        public PartialViewResult CalculatorWindow()
        {
            return PartialView("_CalculatorWindow");
        }

        [HttpPost]
        public JsonResult SaveExaminationSheet(QuestionSetUpViewModel model)
        {
            return Json(onlineExamService.SaveExaminationSheet(new OnlineExam.Request.StudentExaminationSheetResponseDTO()
            {

                LoginID = User.UserId,
                PaperDetailsID = model.PaperDetailsID,
                PaperID = model.PaperID,
                ExaminationDuration = model.ExaminationDuration,
                TotalTime = model.TotalTime
            }, "INSERT"), JsonRequestBehavior.AllowGet);
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
    }
}