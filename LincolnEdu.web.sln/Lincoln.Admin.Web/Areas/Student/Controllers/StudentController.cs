﻿using Lincoln.Admin.Web.Controllers;
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

            questionSectionViewmodel = onlineExamService.GetExamQuestionSection(new OnlineExam.Request.ExamQuestionSectionRequestDTO() {
                CourseID=Convert.ToInt32(id)
            }).
            Select(a => new ExamQuestionSectionViewModel() {
                ExaminationSectionID=a.ExaminationSectionID,
                QuestionNo=a.QuestionNo,
                SectionName=a.SectionName
            }).ToList();
            var tupleData = new Tuple<StudentExamViewModel, List<ExamQuestionSectionViewModel>>(model, questionSectionViewmodel);
            return View(tupleData);
            
        }
    }
}