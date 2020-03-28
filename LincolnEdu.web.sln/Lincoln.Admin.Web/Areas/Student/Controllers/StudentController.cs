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

            model = onlineExamService.GetQuestionPaper(new OnlineExam.Request.ExamQuestionSectionRequestDTO()
            {
                CourseID = Convert.ToInt32(courseID),
                QuestionNo = Convert.ToInt32(questionNo),
                SectionName = sectionName
            })
                .Select(a => new QuestionSetUpViewModel()
                {
                    QuestionType=a.QuestionType,
                    QuestionText =a.QuestionText,
                    OptionANo=a.OptionANo,
                    OptionAText=a.OptionAText,
                    OptionBNo=a.OptionBNo,
                    OptionBText=a.OptionBText,
                    OptionCNo=a.OptionCNo,
                    OptionCText=a.OptionCText,
                    OptionDNo=a.OptionDNo,
                    OptionDText=a.OptionDText,
                    OptionENo=a.OptionENo,
                    OptionEText=a.OptionEText

                }).FirstOrDefault();


            return PartialView("_QuestionSheet", model);
        }

        public PartialViewResult LeftSuggestionPanel()
        {
            return PartialView("_LeftSuggestionPanel");
        }
        public PartialViewResult HeaderButton()
        {
            return PartialView("_HeaderButton");
        }

        [HttpPost]
        public JsonResult SaveExaminationSheet(QuestionSetUpViewModel model)
        {
            return Json("");
        }
    }
}